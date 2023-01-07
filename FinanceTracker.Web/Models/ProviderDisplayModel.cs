using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class ProviderDisplayModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [StringLength(50, ErrorMessage = "{0} must be under {1} characters long.")]
        public string? Title { get; set; }

        [Display(Name = "Reason for Transactions")]
        [StringLength(100, ErrorMessage = "{0} must be under {1} characters long.")]
        public string? Service { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(300, ErrorMessage = "{0} must be under {1} characters long.")]
        public string? Url { get; set; }

        [Required]
        public int PayorId { get; set; }
    }
}
