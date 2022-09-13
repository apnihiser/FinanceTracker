using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class PayorDisplayModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50,ErrorMessage = "{0} must be under 50 characters long.")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} must be under 50 characters long.")]
        public string? LastName { get; set; }
    }
}
