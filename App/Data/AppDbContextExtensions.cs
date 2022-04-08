using App.Models;

namespace App.Data
{
    public static class AppDbContextExtensions
    {
        
        public static void Seed(this AppDbContext dbContext){
            if (dbContext.ResultItems.Any()){

            }
            var resultItems = new List<ResultItem>(){

            };
            dbContext.ResultItems.AddRange(resultItems);
            dbContext.SaveChanges();
        }
    }
}