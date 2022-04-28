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
        [MaxLength(15), MinLength(8), RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; }
    }
}