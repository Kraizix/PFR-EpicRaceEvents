using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using App.Models;
using App.Data;
using App.ViewModels;

namespace App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        string user = HttpContext.Session.GetString("_name") ?? "";
        // Get future Races from and ordered by date





        // var races = _dbContext.Races.ToList();
        // races.Sort((x, y) => x.EventDate.CompareTo(y.EventDate));
        var races = _dbContext.Races.OrderBy(x => x.EventDate).Where(y => y.EventDate > DateTime.Now).ToList();
        var raceListViewModel = new RaceListViewModel(
            races.Take(3),
            user
        );
        return View(raceListViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
