using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class DriversController : Controller
    {
        private readonly AppDbContext _dbContext;
        public DriversController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Drivers
        public ActionResult Index(string searchString)
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
            Console.Write("Salut");
            if (string.IsNullOrEmpty(searchString))
            {
                Console.WriteLine("test");
                var pilots = _dbContext.Pilots.Select(p => new ViewProfile()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate,
                    Mail = p.Mail,
                }).ToList();
                return View("Index", pilots);
            }
            var pilost = _dbContext.Pilots.Where(p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString) || p.Mail.Contains(searchString)).Select(p => new ViewProfile()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Mail = p.Mail,
            }).ToList();
            Console.WriteLine("test2");
            return View("Index", pilost);
        }
        // GET: Drivers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Drivers/Edit/5
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
            return View("Edit");
        }

        // POST: Drivers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditDriver driver)
        {
            Console.WriteLine(id);
            if (HttpContext.Session.GetString("_id") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Id == HttpContext.Session.GetInt32("_id"));
            if (pilot.Admin != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    Pilot user = _dbContext.Pilots.FirstOrDefault(p => p.Id == id);
                    if (!string.IsNullOrEmpty(driver.FirstName))
                    {
                        user.FirstName = driver.FirstName;
                    }
                    if (!string.IsNullOrEmpty(driver.LastName))
                    {
                        user.LastName = driver.LastName;
                    }
                    if (driver.BirthDate != null)
                    {
                        user.BirthDate = driver.BirthDate ?? user.BirthDate;
                    }
                    if (!string.IsNullOrEmpty(driver.Mail))
                    {
                        user.Mail = driver.Mail;
                    }
                    _dbContext.Pilots.Update(user);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                // TODO: Add update logic here
                Console.WriteLine("ici");
                return View("Edit");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("ici2");
                return View("Edit");
            }
        }
    }
}