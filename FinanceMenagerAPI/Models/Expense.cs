using System.ComponentModel.DataAnnotations;

namespace FinanceMenagerAPI.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } //will be enum 

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Cost { get; set; }
    }
}
