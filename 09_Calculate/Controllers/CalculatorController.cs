using _09_Calculate.Data;
using _09_Calculate.Models;
using _09_Calculate.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace _09_Calculate.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly CalculatorContext _context;
        private readonly KafkaProducerService<Null, string> _producer;

        public CalculatorController(CalculatorContext context, KafkaProducerService<Null, string> producer)
        {
            _context = context;
            _producer = producer;
        }

        /// <summary>
        /// Отображение страницы Index с данными из базы.
        /// </summary>
        public IActionResult Index()
        {
            var data = _context.DataInputVariants.OrderByDescending(x => x.ID_DataInputVariant).ToList();
            ViewBag.Data = data; // Передача данных в ViewBag
            return View(); // Возвращаем представление без передачи данных напрямую
        }

        /// <summary>
        /// Обработка запроса на вычисление.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessingCalculationRequest(double num1, double num2, Models.Operation operation)
        {
            double result = 0;
            string errorMessage = null;

            try
            {
                switch (operation)
                {
                    case Models.Operation.Add:
                        result = num1 + num2;
                        break;
                    case Models.Operation.Subtract:
                        result = num1 - num2;
                        break;
                    case Models.Operation.Multiply:
                        result = num1 * num2;
                        break;
                    case Models.Operation.Divide:
                        if (num2 != 0)
                            result = num1 / num2;
                        else
                            errorMessage = "Ошибка: деление на ноль невозможно.";
                        break;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Произошла ошибка: " + ex.Message;
            }

            ViewBag.Result = result;
            ViewBag.Num1 = num1;
            ViewBag.Num2 = num2;
            ViewBag.Operation = operation.ToString();
            ViewBag.ErrorMessage = errorMessage;

            // Создаем объект для передачи в Kafka
            var dataInputVariant = new DataInputVariant
            {
                Operand_1 = num1,
                Operand_2 = num2,
                Type_operation = operation,
                Result = result.ToString()
            };

            // Отправка данных в Kafka
            await SendDataToKafka(dataInputVariant);

            // Сохранение данных в БД
            _context.DataInputVariants.Add(dataInputVariant);
            _context.SaveChanges();

            // Обновляем ViewBag.Data после добавления новой записи
            var updatedData = _context.DataInputVariants.OrderByDescending(x => x.ID_DataInputVariant).ToList();
            ViewBag.Data = updatedData;

            return View("Index");
        }

        public IActionResult Callback([FromBody] DataInputVariant inputData)
        {
            SaveDataAndResult(inputData);
            return Ok();
        }

        /// <summary>
        /// Сохранение данных и результата в базе данных.
        /// </summary>
        private DataInputVariant SaveDataAndResult(DataInputVariant inputData)
        {
            _context.DataInputVariants.Add(inputData);
            _context.SaveChanges();
            return inputData;
        }

        /// <summary>
        /// Отправка данных в Kafka.
        /// </summary>
        private async Task SendDataToKafka(DataInputVariant dataInputVariant)
        {
            var json = JsonSerializer.Serialize(dataInputVariant);
            await _producer.ProduceAsync("krasheninnikov", new Message<Null, string> { Value = json });
        }
    }
}
