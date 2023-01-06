using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Models
{
    public class FullAccountViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? Type { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        [Display(Name = "Account Holder")]
        public int HolderId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Display(Name = "Account Holder")]
        public string? FullName 
        {
            get
            { 
                return $"{FirstName} {LastName}"; 
            }
        }
    }
}
