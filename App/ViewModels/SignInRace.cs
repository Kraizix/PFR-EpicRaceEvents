using App.Models;
using System.ComponentModel.DataAnnotations;
using App.ViewModels.CustomDataAnnotation;

namespace App.ViewModels
{
    public class SignInRace
    {
        [Required]
        public Vehicle vehicle { get; set; }
    }
}