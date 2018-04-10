using System.ComponentModel.DataAnnotations;

namespace AdWorksCore.Web.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "TMI - too much info.")]
        public string Message { get; set; }
    }
}
