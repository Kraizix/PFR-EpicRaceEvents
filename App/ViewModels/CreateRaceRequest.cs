using System.ComponentModel.DataAnnotations;
using App.Models;
using App.ViewModels.CustomDataAnnotation;

namespace App.ViewModels
{
    public class CreateRaceRequest
    {
        [Required(ErrorMessage = "You need to provide a name for your race")]
        [MaxLength(50), MinLength(2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabetical letters.")]
        public string? RaceName { get; set; }

        [Required(ErrorMessage = "You need to specify a date for your race")]
        [DataType(DataType.DateTime)]
        [DateComp]
        public DateTime RaceDate { get; set; }

        [Required(ErrorMessage = "Latitude of the starting point of the race must be speficied")]
        [Range(-90, 90)]
        public float Latitude { get; set; }

        [Required(ErrorMessage = "Longitude of the starting point of the race must be specified")]
        [Range(-180, 180)]
        public float Longitude { get; set; }
        [Range(2, int.MaxValue)]
        public int? MaxParticipants { get; set; }
        [Range(18, 120)]
        public int? AgeRestriction { get; set; }
        public string? Image { get; set; }

        public List<Category> Categories { get; set; }
    }
}