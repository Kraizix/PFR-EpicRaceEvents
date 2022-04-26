using App.Models;

namespace App.Data
{
    public static class AppDbContextExtensions
    {
        
        public static void Seed(this AppDbContext dbContext){
            if (dbContext.ResultItems.Any()){

            }
            // var Races = new List<Race>()
            // {
            //     new Race()
            //     {
            //         Id = 1,
            //         Name = "Race 1",
            //         EventDate = new DateTime(2022, 04, 20, 15, 45, 00),
            //         StartHour = 15,
            //         Latitude = 15,
            //         Longitude = -60,
            //         MaxParticipants = 10,
            //         Image = "https://via.placeholder.com/150",
            //         AgeRestriction = 18,
            //     },
            //     new Race()
            //     {
            //         Id = 2,
            //         Name = "Race 2",
            //         EventDate = new DateTime(2022, 05, 02, 13, 30, 00),
            //         StartHour = 13,
            //         Latitude = 15,
            //         Longitude = -60,
            //         MaxParticipants = 10,
            //         Image = "https://via.placeholder.com/150",
            //         AgeRestriction = 18,
            //     },
            //     new Race()
            //     {
            //         Id = 3,
            //         Name = "Race 3",
            //         EventDate = new DateTime(2022, 06, 02, 13, 40, 00),
            //         StartHour = 13,
            //         Latitude = 40,
            //         Longitude = -20,
            //         MaxParticipants = 15,
            //         Image = "https://via.placeholder.com/150",
            //         AgeRestriction = 16,
            //     },

            // };
            // dbContext.Races.AddRange(Races);
            // dbContext.SaveChanges();
        }
    }
}