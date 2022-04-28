using System.ComponentModel.DataAnnotations;
using App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.ViewModels.CustomDataAnnotation
{
    public class VehiclesData : ValidationAttribute
    {
        public VehiclesData()
        {
            ErrorMessage = "Must Select one vehicle";
        }

        public override bool IsValid(object value)
        {
            if (value is List<SelectListItem>)
            {
                return ((List<SelectListItem>)value).Count > 0;
            }
            return false;
        }
    }
}