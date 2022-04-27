using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.ViewModels;
using App.Models;
using App.Data;
using BCrypt.Net;

namespace App.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _dbContext;
        public LoginController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Console.WriteLine("salut");
                    Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Mail == user.Mail);
                    if (pilot == null)
                    {
                        return View("Index");
                    }
                    if (BCrypt.Net.BCrypt.Verify(user.Password, pilot.Password))
                    {
                        HttpContext.Session.SetString("_name", pilot.FirstName);
                        HttpContext.Session.SetInt32("_id", pilot.Id);
                        HttpContext.Session.SetInt32("_admin", pilot.Admin);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View("Index");
            }
            catch
            {
                Console.WriteLine("Bonjour");
                return View("Index");
            }
        }
    }
}