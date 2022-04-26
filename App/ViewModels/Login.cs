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
        [MaxLength(15), MinLength(8)]
        public string Password { get; set; }
    }
}