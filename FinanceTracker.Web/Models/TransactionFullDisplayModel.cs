using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class TransactionFullDisplayModel
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
        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? Status { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? ProviderName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? Service { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? AccountName { get; set; }
    }
}
