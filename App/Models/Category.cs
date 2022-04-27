namespace App.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public ICollection<Vehicle>? Vehicles { get; set; }
        public ICollection<Race>? Races { get; set; }
    }
}
