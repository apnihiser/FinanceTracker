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
        [Range(0, int.MaxValue, ErrorMessage = "Only numbers greater than zero allowed.")]
        public decimal AmountDue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} must be under 50 characters long.")]
        public string Status { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        [Required]
        [Display(Name = "Transaction Reason")]
        public string Reason { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        [Required]
        [Display(Name = "Withdrawal/ Deposit")]
        public string Type { get; set; }
    }
}
