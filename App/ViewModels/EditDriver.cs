using System.ComponentModel.DataAnnotations;
using App.ViewModels.CustomDataAnnotation;

namespace App.ViewModels
{
    public class EditDriver
    {
        [MaxLength(30), MinLength(2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabetical letters.")]
        public string? FirstName { get; set; }
        [MaxLength(30), MinLength(2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabetical letters.")]
        public string? LastName { get; set; }
        [AgeComp]
        public DateTime? BirthDate { get; set; }
        [EmailAddress]
        public string? Mail { get; set; }
    }
}