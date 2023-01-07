using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class AccountDisplayModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account Holder")]
        public int HolderId { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {1} and {2} digits long.")]
        public string? Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be between {1} and {2} digits long.")]
        public string? Description { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {1} and {2} digits long.")]
        public string? Type { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}
