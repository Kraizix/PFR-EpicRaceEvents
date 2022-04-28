using App.Data;
using App.Models;
using App.ViewModels;
using Microsoft.EntityFrameworkCore;
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
            var races = _dbContext.Races.Include(x => x.Pilots).OrderBy(r => r.EventDate).ToList();

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
            var race = _dbContext.Races.Include(x => x.Pilots).First(r => r.Id == id);
            return View("RaceDetails", race);
        }

        // GET: SignIn
        public ActionResult SignIn(int id)
        {
            if (HttpContext.Session.GetString("_id") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var race = _dbContext.Races.Include(x => x.AuhtorizedCategories).Include(x => x.Pilots).First(r => r.Id == id);
            Pilot pilot = _dbContext.Pilots.Include(x => x.Vehicles).ThenInclude(x => x.Categories).First(p => p.Id == (int)HttpContext.Session.GetInt32("_id"));
            if (race.AgeRestriction > (race.EventDate.Year - pilot.BirthDate.Year))
            {
                return RedirectToAction("Index");
            }
            if (race.EventDate < DateTime.Now)
            {
                return RedirectToAction("Index");
            }
            int pilotsCount = 0;
            try{
                pilotsCount = race.Pilots.Count;
            }catch{}
            if (race.MaxParticipants == pilotsCount){
                return RedirectToAction("Index");
            }
            foreach(Pilot p in race.Pilots){
                if(p.Id == pilot.Id){
                    return RedirectToAction("Index");
                }
            }
            List<SelectListItem> vehicleList = new();
            vehicleList.Insert(0, new SelectListItem ("Please Select a vehicle...", "none" ));
            foreach (Vehicle vehicle in pilot.Vehicles){
                foreach(var rc in race.AuhtorizedCategories){
                    foreach(var vc in vehicle.Categories){
                        if (vc == rc){
                            vehicleList.Add(new SelectListItem(vehicle.Brand + vehicle.Model, vehicle.Id.ToString(), false, false));
                            goto NextVehicle;
                        }
                    }
                }
                vehicleList.Add(new SelectListItem (vehicle.Brand +" "+ vehicle.Model, vehicle.Id.ToString(), false, true ));
                NextVehicle:
                continue;
            }
            SignIn SignInModel = new()
            {
                Race = race,
                VehicleList = vehicleList
            };
            return View("SignIn", SignInModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignInRace(Race race, string vehicleList)
        {
            if (vehicleList == "none"){
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine(vehicleList);
            Pilot pilot = _dbContext.Pilots.First(p => p.Id == (int)HttpContext.Session.GetInt32("_id"));
            foreach (Pilot p in race.Pilots)
            {
                if (p.Id == pilot.Id)
                {
                    return RedirectToAction("Index");
                }
            }
            race.Pilots.Add(pilot);
            //_dbContext.Races.Update(race);
            //_dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Races/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("_id") == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Cats = new MultiSelectList(_dbContext.Categories, "Id", "Name");
            return View("CreateRace");
        }

        // POST: Races/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRaceRequest race, List<uint> Cats)
        {
            race.Categories = new List<Category>(Cats.Count);
            foreach (int id in Cats)
            {
                Console.WriteLine("\n" + id);
                race.Categories.Add(_dbContext.Categories.FirstOrDefault(c => c.Id == id));
            }
            ModelState.Clear();
            TryValidateModel(race);
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
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
                        Image = race.Image ?? "https://www.driving.co.uk/wp-content/uploads/sites/5/2019/02/2019-Daytona-500-NASCAR-Massive-Crash-01.jpg",
                        AuhtorizedCategories = race.Categories,
                    };
                    Console.WriteLine(newRace);
                    _dbContext.Races.Add(newRace);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Cats = new MultiSelectList(_dbContext.Categories, "Id", "Name");
                return View("CreateRace");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ViewBag.Cats = new MultiSelectList(_dbContext.Categories, "Id", "Name");
                return View("CreateRace");
            }
        }

        // GET: Races/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("_id") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Id == HttpContext.Session.GetInt32("_id"));
            if (pilot.Admin != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Cats = new MultiSelectList(_dbContext.Categories, "Id", "Name");
            return View("Edit");
        }

        // POST: Races/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateRaceRequest raceRequest)
        {
            if (HttpContext.Session.GetString("_id") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Id == HttpContext.Session.GetInt32("_id"));
            if (pilot.Admin != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            Race race = _dbContext.Races.FirstOrDefault(r => r.Id == id);
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