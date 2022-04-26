using System.ComponentModel.DataAnnotations;
using App.ViewModels.CustomDataAnnotation;

namespace App.ViewModels
{
    public class EditProfile
    {
        [Required]
        [MaxLength(30), MinLength(2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabetical letters.")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30), MinLength(2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabetical letters.")]
        public string LastName { get; set; }
    }
}