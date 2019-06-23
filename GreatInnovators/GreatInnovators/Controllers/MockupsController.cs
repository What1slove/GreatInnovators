using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GreatInnovators.Controllers
{
    public class MockupsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
        public IActionResult Order()
        {
            return this.View();
        }

        public IActionResult History()
        {
            return this.View();
        }
    }
}
