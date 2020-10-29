using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VuelaLibre.Models;
using VuelaLibre.Models.Maps;

namespace VuelaLibre.Controllers
{
    public class AccountController : Controller
    {
        private readonly VuelaLibreContext _context;
        private readonly IConfiguration configuration;

        public AccountController(VuelaLibreContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index(Account account)
        {
            //ViewBag.Accounts = _context.Accounts.ToList();
            return View("Index");
        }
        [HttpGet]
        public ActionResult Register() // GET
        {
            return View("Register", new Account());
        }

        [HttpPost]
        public ActionResult Register(Account account, string contraseña, string correo, string verfcontraseña) // POST
        {
            
            var correos = _context.Accounts.ToList();
            foreach (var item in correos)
            {
                if (item.Correo == correo)
                    ModelState.AddModelError("Correo","El correo ya existe, ingrese otro correo");
            }
           
            if (ModelState.IsValid)
            {
              
                
                account.Contraseña = CreateHash(contraseña);
                account.VerfContraseña = CreateHash(verfcontraseña);
                account.saldo = 300;
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View("Register", account);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Account account, string Correo, string Contraseña)
        {
            var user = _context.Accounts.Where(o => o.Correo == Correo && o.Contraseña == CreateHash(Contraseña))
                .FirstOrDefault();
            ViewBag.iduser = user.Id;
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Correo)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(claimsPrincipal);
                ViewBag.userid = user.Id;
                
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Login", "Usuario o contraseña incorrectos.");
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]

        public ActionResult CrearVuelo() // GET
        {
            ViewBag.Destinos = _context.ListVuelo.ToList();
            ViewBag.Aerolineas = _context.Aerolineas.ToList();
            ViewBag.fechas = DateTime.Now;
            ViewBag.fecha = DateTime.Now;
            return View("CrearVuelo", new Vuelo());
        }
        
        [HttpPost]
        public ActionResult CrearVuelo(Vuelo vuel, DateTime fechaida) // POST
        {
            var now = DateTime.Now;
            int fechaResultado = DateTime.Compare(fechaida, now);
            if (fechaResultado < 0)
                ModelState.AddModelError("FechaIda", "La fecha de salida debe ser mayor a la actual");
            
           

            if (ModelState.IsValid)
            {
                _context.Vuelos.Add(vuel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Destinos = _context.ListVuelo.ToList();
            ViewBag.Aerolineas = _context.Aerolineas.ToList();
            return View("CrearVuelo");
        }

        [HttpGet]
        public ActionResult MostrarVuelos()
        {
            DateTime fechaActual = DateTime.Now;
            var vuelo =  _context.Vuelos.Where(o=>o.FechaIda>=fechaActual).ToList();
           // ViewBag.vuelos = _context.Vuelos.ToList();
            return View(vuelo);
        }
        [HttpPost]
        public ActionResult Buscar(Vuelo vuelo, string origen,string destino, DateTime fechaida, int iduser)
        {
            
            var mostrar = _context.Vuelos.Where(o => o.Origen == origen && o.Destino == destino && o.FechaIda >= fechaida)
                .ToList();
          
            if (mostrar != null)
            {
                var now = DateTime.Now;
                int fechaResultado = DateTime.Compare(fechaida, now);
                if (fechaResultado < 0)
                   ModelState.AddModelError("FechaIda22", "La fecha de salida debe ser mayor a la actual");
                if (fechaida == null)
                    ModelState.AddModelError("FechaIda", "El campo fecha es requerido");
                if (origen == null)
                    ModelState.AddModelError("Origen", "El campo origen es requerido");
                if (destino == null)
                    ModelState.AddModelError("Destino", "El campo destino es requerido");
                ViewBag.userid = iduser;
               // return View("Index", vuelo);
            }
            if (ModelState.IsValid)
            {
                return View(mostrar);
            }
            else
            {
                return View("Index", vuelo);
            }
                        
            //return View(mostrar);
        }
        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Comprar(Vuelo vuelo)
        {
            var vuelos = _context.Vuelos.Where(o=>o.Id==vuelo.Id).FirstOrDefault();
            return View(vuelos);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Comprar(Vuelo vuelo, int id, int userid, string dni, string nombre, string apellidos, int Npasajes)
        {

            var mostrar = _context.Vuelos.Where(o => o.Id == id).ToList();
            var vuel = _context.Vuelos.Where(o => o.Id == id).FirstOrDefault();
            var usuario = _context.Accounts.Where(o=>o.Id==LoggerUser().Id).FirstOrDefault();
            
           // var account = _context.Accounts.Where(o => o.Id == userid).FirstOrDefault();
            var detallev = new DetalleVuelo();
            if (vuel.TotalPasaje <= 0)
            {
               // ModelState.AddModelError("Tpasaje", "Ya no quedan pasajes");
                TempData["Tpasaje"] = "Lo sentimos, no quedan pasajes disponibles para este vuelo";
                 return View(vuel);
            }
            if (usuario.saldo <= vuel.Precio)
            {
                // ModelState.AddModelError("Tpasaje", "Ya no quedan pasajes");
                TempData["Tsueldo"] = "Lo sentimos, no le queda saldo suficiente";
                return View(vuel);
            }

            //var ide2 = LoggerUser().Id;
            if (vuel!=null)
            {

                usuario.saldo = usuario.saldo - vuel.Precio;
                detallev.FechaSalida = vuel.FechaIda;
                detallev.UserId =LoggerUser().Id;
                detallev.Aerolinea = vuel.Aerolinea;
                detallev.Nombres = nombre;
                detallev.Apellidos = apellidos;
                detallev.Dni = dni;
                detallev.precio = vuel.Precio;
                detallev.Nasiento = Npasajes;
                detallev.Origen = vuel.Origen;
                detallev.Destino = vuel.Destino;
                detallev.FechaSalida = vuel.FechaIda;
                vuel.TotalPasaje = vuel.TotalPasaje - 1;
                
                //
                _context.DetalleVuelos.Add(detallev);
                _context.SaveChanges();
                //detallev.Apellidos

                //_context.DetalleVuelos.Add(detallev);

                return RedirectToAction("Index");
            }
            return View("comprar", new Vuelo()) ;
        }

        
        [Authorize]
        public ActionResult Perfil ()
        {
            ViewBag.Accounts = _context.Accounts.Where(o=>o.Id==LoggerUser().Id).FirstOrDefault(); ;
            ViewBag.DetalleVuelo = _context.DetalleVuelos.Where(o => o.UserId == LoggerUser().Id).ToList();
            return View();
        }
        public Account LoggerUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = _context.Accounts.Where(o => o.Correo == claim.Value).FirstOrDefault();
            return user;
        }
      


    }
}
