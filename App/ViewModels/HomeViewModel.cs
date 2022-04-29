using App.Models;

namespace App.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Race> RaceList { get; }
        public string HeaderTitle { get; }
        public RaceResult Result { get; }
        public HomeViewModel(IEnumerable<Race> races, string headerTitle, RaceResult result)
        {
            RaceList = races;
            HeaderTitle = headerTitle;
            Result = result;
        }
    }
}