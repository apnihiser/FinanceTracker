using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public int AccountId { get; set; }

        [Required]
        [Display(Name = "Provider Name")]
        public int PayeeId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AmountDue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? Status { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? ProviderName { get; set; }

        [StringLength(100, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? Service { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? AccountName { get; set; }

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
