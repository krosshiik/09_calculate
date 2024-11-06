using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _09_Calculate.Models;

namespace _09_Calculate.Data
{
    public class DataInputVariant
    {
        [Key]
        public int ID_DataInputVariant { get; set; } // Unique identifier for the DataInputVariant object

        public double Operand_1 { get; set; } // First operand for the operation

        public double Operand_2 { get; set; } // Second operand for the operation

        public Operation Type_operation { get; set; } // Type of operation (e.g., addition, subtraction, etc.)

        [Column(TypeName = "varchar(128)")]
        public string? Result { get; set; } // Result of the operation
    }
}
