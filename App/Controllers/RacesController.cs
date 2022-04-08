using App.Data;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class RacesController : Controller
    {
        private readonly AppDbContext _dbContext;
        public RacesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Races
        public ActionResult Index()
        {
            var races = _dbContext.Races.ToList();
            //var races = new List<Race>()
            //{
            //    new Race()
            //    {
            //        Id = 1,
            //        Name = "Ma course 123",
            //        EventDate = new DateTime(2022, 04, 02)
            //    },
            //    new Race()
            //    {
            //        Id = 2,
            //        Name = "Ma super pas course",
            //        EventDate = new DateTime(2022, 02, 02)
            //    },
            //    new Race()
            //    {
            //        Id = 3,
            //        Name= "Ma course pourrie",
            //        EventDate = new DateTime(2022, 04, 02)
            //    }
            //};

            var raceListViewModel = new RaceListViewModel(
                races,
                "Liste de courses"
            );

            return View("RaceList", raceListViewModel);
        }

        // GET: Races/
        public ActionResult List()
        {
            return Ok("LIST ACTION CALLED !");
        }

        // GET: Races/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Races/Create
        public ActionResult Create()
        {

            return View("CreateRace");
        }

        // POST: Races/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRaceRequest race)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Race newRace = new()
                    {
                        Name = race.RaceName,
                        EventDate = race.RaceDate
                    };

                    _dbContext.Races.Add(newRace);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                return View("CreateRace");
            }
            catch
            {
                return View("CreateRace");
            }
        }

        // GET: Races/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Races/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Races/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Races/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}