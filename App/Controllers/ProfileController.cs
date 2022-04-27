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
    public class ProfileController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ProfileController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Profile
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("_id") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Id == HttpContext.Session.GetInt32("_id"));
            ViewProfile model = new()
            {
                FirstName = pilot.FirstName,
                LastName = pilot.LastName,
                BirthDate = pilot.BirthDate,
                Mail = pilot.Mail,
            };
            return View("Index", model);
        }
        // GET: Profile/Edit
        public ActionResult Edit()
        {
            if (HttpContext.Session.GetString("_id") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View("Edit");
        }

        // POST: Profile/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProfile profile)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (HttpContext.Session.GetString("_id") == null)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Id == HttpContext.Session.GetInt32("_id"));
                    if (!string.IsNullOrEmpty(profile.FirstName))
                    {
                        pilot.FirstName = profile.FirstName;
                    }
                    if (!string.IsNullOrEmpty(profile.LastName))
                    {
                        pilot.LastName = profile.LastName;
                    }
                    _dbContext.Pilots.Update(pilot);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("Edit");
            }
            catch
            {
                return View("Edit");
            }
        }
    }
}