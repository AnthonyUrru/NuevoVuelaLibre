using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VuelaLibre.Controllers
{
    public class CompraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
