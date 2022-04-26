using System.ComponentModel.DataAnnotations;

namespace App.ViewModels.CustomDataAnnotation
{
    public class AgeComp : ValidationAttribute
    {
        public AgeComp()
        {
            ErrorMessage = " > 16 years";
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return ((new DateTime(1, 1, 1) + (DateTime.Now - date)).Year - 1) > 16;
            }

            return false;
        }
    }
}