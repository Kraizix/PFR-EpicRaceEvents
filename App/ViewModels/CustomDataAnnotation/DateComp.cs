using System.ComponentModel.DataAnnotations;

namespace App.ViewModels.CustomDataAnnotation
{
    public class DateComp : ValidationAttribute
    {
        public DateComp()
        {
            ErrorMessage = "The date must be in the future.";
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }

            return false;
        }
    }
}