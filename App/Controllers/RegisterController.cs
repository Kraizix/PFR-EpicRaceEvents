using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.ViewModels;
using App.Models;
using App.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Controllers
{
    public class RegisterController : Controller
    {
        private static Random rnd = new Random();
        private readonly AppDbContext _dbContext;
        public RegisterController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Register
        public ActionResult Index()
        {
            return View("Index");
        }
        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Register user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    List<Vehicle> vehicles = new List<Vehicle>();
                    Vehicle vhc = _dbContext.Vehicles.Include(x => x.Categories).ToList().OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    vehicles.Add(vhc);
                    Pilot newPilot = new()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BirthDate = user.BirthDate,
                        Mail = user.Mail,
                        Password = user.Password,
                        Vehicles = vehicles

                    };
                    _dbContext.Pilots.Add(newPilot);
                    _dbContext.SaveChanges();
                    HttpContext.Session.SetString("_name", newPilot.FirstName);
                    HttpContext.Session.SetInt32("_id", newPilot.Id);
                    Console.WriteLine(newPilot.Id);

                    return View("ShowVehicle", vhc);
                }
                return View("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}