using Assigment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class WeightliftingController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public WeightliftingController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Weighlifting_Session()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}