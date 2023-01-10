using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account")]
        public int AccountId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int PayeeId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only numbers greater than zero allowed.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Dollar Amount")]
        public decimal AmountDue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? Status { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        [Display(Name = "Category")]
        public string? ProviderName { get; set; }

        [StringLength(100, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? Service { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        [Display(Name = "Account")]
        public string? AccountName { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        [Required]
        [Display(Name = "Transfer Description")]
        public string Reason { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        [Required]
        [Display(Name = "Withdrawal/ Deposit")]
        public string Type { get; set; }
    }
}
