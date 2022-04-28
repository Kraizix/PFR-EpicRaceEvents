using System.ComponentModel.DataAnnotations;
using App.ViewModels.CustomDataAnnotation;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Models;

namespace App.ViewModels
{
    public class SignIn
    {
        [Required]
        public List<SelectListItem> VehicleList { get; set; }

        [Required]
        public Race Race { get; set; }
    }
}