using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEVTEST_Cameron_MVC.Models
{
    public class Salesperson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 120)]
        public int? Age { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Salary { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}