using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _09_Calculate.Data
{
    public class DataInputVariant
    {
        [Key]
        public int ID_DataInputVariant { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string? Operand_1 { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string? Operand_2 { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string? Type_operation { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string? Result { get; set; }
    }

}
