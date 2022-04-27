namespace App.Models
{
    public class Pilot
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Race> Races { get; set; }
        public int Admin { get; set; }
    }
}