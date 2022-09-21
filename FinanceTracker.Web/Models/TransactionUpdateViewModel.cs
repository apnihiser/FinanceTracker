using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class TransactionUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public int AccountId { get; set; }

        [Required]
        [Display(Name = "Provider Name")]
        public int PayeeId { get; set; }

        [Required]
        public decimal AmountDue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} must be under 50 characters long.")]
        public string Status { get; set; } = "Due";
    }
}
