using System.ComponentModel.DataAnnotations;

namespace DEVTEST_Cameron_MVC.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(5)]
        public string? IndustryType { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}