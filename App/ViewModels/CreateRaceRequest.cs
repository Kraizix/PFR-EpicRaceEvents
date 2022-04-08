using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class CreateRaceRequest
    {
        [Required(ErrorMessage = "Vous devez renseigner un nom à votre course")]
        [MaxLength(10)]
        public string? RaceName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RaceDate { get; set; }
    }
}