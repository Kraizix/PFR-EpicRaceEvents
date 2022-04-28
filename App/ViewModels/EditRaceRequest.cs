using System.ComponentModel.DataAnnotations;
using App.Models;
using App.ViewModels.CustomDataAnnotation;

namespace App.ViewModels
{
    public class EditRaceRequest
    {
        [MaxLength(50), MinLength(2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabetical letters.")]
        public string? RaceName { get; set; }

        [DataType(DataType.DateTime)]
        [DateComp]
        public DateTime? RaceDate { get; set; }

        [Range(-90, 90)]
        public float? Latitude { get; set; }

        [Range(-180, 180)]
        public float? Longitude { get; set; }
        [Range(2, int.MaxValue)]
        public int? MaxParticipants { get; set; }
        [Range(18, 120)]
        public int? AgeRestriction { get; set; }
        public string? Image { get; set; }

        public List<Category>? Categories { get; set; }
    }
}