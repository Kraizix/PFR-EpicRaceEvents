using System.ComponentModel.DataAnnotations;
using App.ViewModels.CustomDataAnnotation;

namespace App.ViewModels
{
    public class Register
    {
        [Required]
        [MaxLength(30), MinLength(2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabetical letters.")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30), MinLength(2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabetical letters.")]
        public string LastName { get; set; }
        [Required]
        [AgeComp]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [MaxLength(15), MinLength(8)]
        public string Password { get; set; }
    }
}