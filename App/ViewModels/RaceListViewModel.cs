using App.Models;

namespace App.ViewModels
{
    public class RaceListViewModel
    {
        public IEnumerable<Race> RaceList { get; }
        public string HeaderTitle { get; }
        public RaceListViewModel(IEnumerable<Race> races, string headerTitle)
        {
            RaceList = races;
            HeaderTitle = headerTitle;
        }
    }
}