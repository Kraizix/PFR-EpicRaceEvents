using System.ComponentModel.DataAnnotations;
using App.ViewModels.CustomDataAnnotation;

namespace App.ViewModels
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [MaxLength(8), MinLength(15)]
        public string Password { get; set; }
    }
}