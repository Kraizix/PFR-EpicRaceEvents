namespace App.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int BuildYear { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Power { get; set; }
        public string Image { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Pilot> Pilots { get; set; }
    }
}