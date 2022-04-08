namespace App.Models
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public int StartHour { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int MaxParticipants { get; set; }
        public string Image { get; set; }
        public ICollection<Pilot> Pilots { get; set; }
        public int AgeRestriction { get; set; }
        public ICollection<Category> AuhtorizedCategories { get; set; }
        public RaceResult? Result { get; set; }
    }
}