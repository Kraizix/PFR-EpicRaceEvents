using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using App.Models;
using App.Data;
using App.ViewModels;
using Microsoft.EntityFrameworkCore;

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
        var races = _dbContext.Races.OrderBy(x => x.EventDate).Where(y => y.EventDate > DateTime.Now).ToList();
        var homeViewModel = new HomeViewModel(
            races.Take(3),
            user,
            _dbContext.RaceResults.Include(x => x.ResultItems).OrderByDescending(x => x.Id).FirstOrDefault()
        );
        return View(homeViewModel);
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
