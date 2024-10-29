using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _09_Calculate.Data;

namespace _09_Calculate.Controllers
{
    public enum Operation { Add, Subtract, Multiply, Divide }

    public class CalculatorController : Controller
    {
        private CalculatorContext _context;

        public CalculatorController(CalculatorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate(double num1, double num2, Operation operation)
        {
            double result = 0;
            string errorMessage = null;

            try
            {
                switch (operation)
                {
                    case Operation.Add:
                        result = num1 + num2;
                        break;
                    case Operation.Subtract:
                        result = num1 - num2;
                        break;
                    case Operation.Multiply:
                        result = num1 * num2;
                        break;
                    case Operation.Divide:
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        else
                        {
                            errorMessage = "Ошибка: деление на ноль невозможно.";
                        }
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

            // Создаем экземпляр класса DataInputVariant и заполняем его
            var dataInputVariant = new DataInputVariant
            {
                Operand_1 = num1.ToString(),
                Operand_2 = num2.ToString(),
                Type_operation = operation.ToString(),
                Result = result.ToString()
            };

            _context.DataInputVariants.Add(dataInputVariant);
            _context.SaveChanges();

            return View("Index");
        }

    }
}
