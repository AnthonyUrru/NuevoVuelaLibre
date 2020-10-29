using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VuelaLibre.Controllers
{
    public class TravelsController : Controller
    {
        public ViewResult Search()
        {   

            return View("Search");
        }
    }
}
