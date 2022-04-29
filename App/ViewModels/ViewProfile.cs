using App.Models;

namespace App.ViewModels
{
    public class ViewProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mail { get; set; }
        public ICollection<Race> Races { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}