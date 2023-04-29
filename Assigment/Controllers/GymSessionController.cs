using Assigment.Database;
using Assigment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Assigment.Controllers
{
    public class GymSessionController : Controller
    {
        private readonly MyDbContext _context;

        public GymSessionController(MyDbContext context)
        {
            _context = context;
        }

        gymSession myGymSession = new gymSession();
        public List<gymSession> GetGymSessions()
        {
            // Replace this with actual data from a database or API
            var sessions = new List<gymSession>
            {
                new gymSession { gymSession_ID = 1, gymSession_Category = "Weights", gymSession_TrainerName = "Mark Otto", 
                    gymSession_Date= DateTime.Now, gymSession_Slots = 5 },
            };

            return sessions;
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var sessionToDelete = _context.GymSessions.FirstOrDefault(s => s.gymSession_ID == id);
            if (sessionToDelete != null)
            {
                _context.GymSessions.Remove(sessionToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index","GymSession");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var models = _context.GymSessions.ToList();
            return View(models);
        }


        public IActionResult AddGymSession()
        {


            return View();
        }

        [HttpPost]
        public IActionResult ProcessAdditonalData(gymSession gymSession)
        {
            

            _context.GymSessions.Add(gymSession);
            _context.SaveChanges();

            return RedirectToAction("Index", "GymSession");
        }


    }
}
