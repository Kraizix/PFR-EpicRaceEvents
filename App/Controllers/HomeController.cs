using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
        var races = _dbContext.Races.ToList();
        races.Sort((x, y) => x.EventDate.CompareTo(y.EventDate));
        var raceListViewModel = new RaceListViewModel(
            races.Take(3),
            "Liste de courses"
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
