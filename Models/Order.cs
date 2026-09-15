using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEVTEST_Cameron_MVC.Models
{
    public class Order
    {
        [Key]
        public int SalesOrder { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustId { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        [ForeignKey(nameof(Salesperson))]
        public int SalesPersonId { get; set; }
        public Salesperson? Salesperson { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}