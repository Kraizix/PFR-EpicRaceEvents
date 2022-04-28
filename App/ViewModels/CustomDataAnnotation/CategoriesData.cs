using System.ComponentModel.DataAnnotations;
using App.Models;

namespace App.ViewModels.CustomDataAnnotation
{
    public class CategoriesData : ValidationAttribute
    {
        public CategoriesData()
        {
            ErrorMessage = "Must Select one category";
        }

        public override bool IsValid(object value)
        {
            if (value is List<Category>)
            {
                return ((List<Category>)value).Count > 0;
            }
            return false;
        }
    }
}