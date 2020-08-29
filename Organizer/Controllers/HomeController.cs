using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Organizer.Controllers
{
    public class HomeController : BassController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
