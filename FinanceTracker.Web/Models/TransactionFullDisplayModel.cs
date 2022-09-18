using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class TransactionFullDisplayModel
    {
        public int Id { get; set; }

        [Display(Name = "Account Name")]
        public int AccountId { get; set; }

        [Display(Name = "Provider Name")]
        public int PayeeId { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountDue { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }

        [StringLength(10, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? Status { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? ProviderName { get; set; }

        [StringLength(100, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? Service { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? AccountName { get; set; }
    }
}
