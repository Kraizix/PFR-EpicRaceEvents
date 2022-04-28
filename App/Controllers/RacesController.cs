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
            var race = _dbContext.Races.Include(x => x.Pilots).Include(x => x.Result).ThenInclude(x => x.ResultItems).First(r => r.Id == id);
            Console.WriteLine(race);
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
            // if (race.AgeRestriction > (race.EventDate.Year - pilot.BirthDate.Year))
            // {
            //     Console.WriteLine("YOOOOOO");
            //     return RedirectToAction("Index");
            // }
            if (race.EventDate < DateTime.Now)
            {
                Console.WriteLine("ici");
                return RedirectToAction("Index");
            }
            int pilotsCount = 0;
            try
            {
                pilotsCount = race.Pilots.Count;
            }
            catch { }
            if (race.MaxParticipants == pilotsCount)
            {
                return RedirectToAction("Index");
            }
            foreach (Pilot p in race.Pilots)
            {
                if (p.Id == pilot.Id)
                {
                    return RedirectToAction("Index");
                }
            }
            List<SelectListItem> vehicleList = new();
            foreach (Vehicle vehicle in pilot.Vehicles)
            {
                foreach (var rc in race.AuhtorizedCategories)
                {
                    foreach (var vc in vehicle.Categories)
                    {
                        if (vc == rc)
                        {
                            vehicleList.Add(new SelectListItem(vehicle.Brand + vehicle.Model, vehicle.Id.ToString(), false, false));
                            goto NextVehicle;
                        }
                    }
                }
                vehicleList.Add(new SelectListItem(vehicle.Brand + " " + vehicle.Model, vehicle.Id.ToString(), false, true));
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
        public ActionResult SignInRace(Race race, int vehicleList)
        {
            Console.WriteLine(vehicleList);
            Pilot pilot = _dbContext.Pilots.First(p => p.Id == (int)HttpContext.Session.GetInt32("_id"));
            Console.WriteLine("test1 : " + race.Pilots.Count());
            foreach (Pilot p in race.Pilots)
            {
                Console.WriteLine("test2");
                if (p.Id == pilot.Id)
                {
                    Console.WriteLine("test3");
                    return RedirectToAction("Index");
                }
            }
            race.Pilots.Add(pilot);
            _dbContext.Races.Update(race);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Races/Create
        public ActionResult Create()
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
            return View("CreateRace");
        }

        // POST: Races/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRaceRequest race, List<uint> Cats)
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
        public ActionResult Edit(int id, EditRaceRequest raceRequest, List<int> Cats)
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
            Race race = _dbContext.Races.Include(x => x.AuhtorizedCategories).FirstOrDefault(r => r.Id == id);
            List<Category> categories = new List<Category>(Cats.Count);
            foreach (int cid in Cats)
            {
                Console.WriteLine("\n" + cid);
                categories.Add(_dbContext.Categories.FirstOrDefault(c => c.Id == cid));
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (raceRequest.RaceName != null)
                    {
                        race.Name = raceRequest.RaceName;
                    }
                    if (raceRequest.RaceDate != null)
                    {
                        race.EventDate = raceRequest.RaceDate ?? race.EventDate;
                    }
                    if (raceRequest.Latitude != null)
                    {
                        race.Latitude = raceRequest.Latitude ?? race.Latitude;
                    }
                    if (raceRequest.Longitude != null)
                    {
                        race.Longitude = raceRequest.Longitude ?? race.Longitude;
                    }
                    if (raceRequest.MaxParticipants != null)
                    {
                        race.MaxParticipants = raceRequest.MaxParticipants ?? race.MaxParticipants;
                    }
                    if (raceRequest.AgeRestriction != null)
                    {
                        race.AgeRestriction = raceRequest.AgeRestriction ?? race.AgeRestriction;
                    }
                    if (categories.Count != 0)
                    {
                        foreach (var category in race.AuhtorizedCategories)
                        {
                            _dbContext.Races.FirstOrDefault(r => r.Id == id).AuhtorizedCategories.Remove(category);
                        }
                        _dbContext.SaveChanges();
                        race.AuhtorizedCategories = categories;
                    }
                    _dbContext.Races.Update(race);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                ViewBag.Cats = new MultiSelectList(_dbContext.Categories, "Id", "Name");
                return View("Edit");
            }
            catch
            {
                Console.WriteLine("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
                ViewBag.Cats = new MultiSelectList(_dbContext.Categories, "Id", "Name");
                return View("Edit");
            }
        }
        public ActionResult GenerateResults(int id)
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

            RaceResult result = new RaceResult();
            result.RaceId = id;
            result.Race = _dbContext.Races.Include(r => r.Pilots).Include(r => r.Result).FirstOrDefault(r => r.Id == id);
            result.ResultItems = new List<ResultItem>();
            List<Pilot> pilots = result.Race.Pilots.OrderBy(x => Guid.NewGuid()).ToList();
            if (result.Race.Result != null)
            {
                Console.WriteLine("Salut");
                return RedirectToAction(nameof(Index));
            }
            if (pilots.Count < 2)
            {
                return RedirectToAction(nameof(Index));
            }
            int i = 1;
            foreach (Pilot p in pilots)
            {
                result.ResultItems.Add(new ResultItem { DriverName = p.FirstName + " " + p.LastName, Rank = i });
                i++;
            }
            _dbContext.RaceResults.Add(result);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}