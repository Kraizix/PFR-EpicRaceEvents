using App.Data;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var races = _dbContext.Races.OrderBy(r => r.EventDate).ToList();
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
            var race = _dbContext.Races.First(r => r.Id == id);
            Console.WriteLine(race);
            return View("RaceDetails", race);
        }

        // GET: SignIn
        public ActionResult SignIn(int id)
        {
            var race = _dbContext.Races.First(r => r.Id == id);
            if (HttpContext.Session.GetString("_id") == null)
            {
                return RedirectToAction("Index","Login");
            }
            else{
                Pilot pilot = _dbContext.Pilots.First(p => p.Id == (int)HttpContext.Session.GetInt32("_id"));
                if (race.AgeRestriction > (DateTime.Now.Subtract(pilot.BirthDate).Days/365)){
                    return RedirectToAction("Index");
                }

                int pilotsCount = 0;
                try{
                    pilotsCount = race.Pilots.Count();
                }catch{}

                if (race.MaxParticipants == pilotsCount){
                    return RedirectToAction("Index");
                }
            }
            return View("SignIn", race);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInRace race)
        {
            Pilot pilot = _dbContext.Pilots.First(p => p.Id == (int)HttpContext.Session.GetInt32("_id"));
            List<SelectListItem> vehicleList = new List<SelectListItem>();
            foreach (Vehicle v in pilot.Vehicles){
                vehicleList.Add(new SelectListItem { Value = v.Id.ToString(), Text = v.Brand + v.Model });
            }
            ViewBag.vehicleList = vehicleList;
            return View("RaceList");
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
                        Name = race.RaceName ?? "",
                        EventDate = race.RaceDate,
                        StartHour = race.RaceDate.Hour,
                        Latitude = race.Latitude,
                        Longitude = race.Longitude,
                        MaxParticipants = race.MaxParticipants ?? 15,
                        AgeRestriction = race.AgeRestriction ?? 21,
                        Image = race.Image ?? "https://www.driving.co.uk/wp-content/uploads/sites/5/2019/02/2019-Daytona-500-NASCAR-Massive-Crash-01.jpg"
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