using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ristorante.Data;
using Ristorante.Models;
using Ristorante.Repository;

namespace Ristorante.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RistoranteRepository _ristoranteRepository;


        public HomeController(ILogger<HomeController> logger, RistoranteRepository ristoranteRepository)
        {
            _logger = logger;
            _ristoranteRepository = ristoranteRepository;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrati()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrazione(string username, string password, string password1)
        {
            bool userRegistered = _ristoranteRepository.Registered(username, password, password1);

            if (userRegistered == true)
            {
                Startup.LoggedIn = 1;
                Startup.Username = username;
                ViewData["info1"] = "Grazie per la tua registrazione, benvenuto ";
            }
            else
            {
                ViewData["info"]= "Registrazione non riuscita, dati non corretti, riprova";
                Startup.LoggedIn = 2;
            }
            return View("Registrati");
        }


            [HttpPost]
        public IActionResult Login(string username, string password)
        {
            bool userLogged = _ristoranteRepository.IsLogged(username, password);
            if (userLogged == true) 
            {
                Startup.LoggedIn = 1;
                Startup.Username = username;
            }
            
            return View("Index");
        }

        public IActionResult Logout()
        {
            Startup.LoggedIn = 0;

            return View("Index");
        }

        public IActionResult Piatti()
        {
            //ViewBag.LoggedIn = false;

            List<Piatto> listaPiatti = _ristoranteRepository.VediPiatti();
            return View(listaPiatti);
        }
        public IActionResult Prenota()
        {

            return View();
        }


        [HttpGet]
        public IActionResult Prenotazione(DateTime data, int posti, string orario, string telefono, string username)
        {
            
            int IdPrenotazione = _ristoranteRepository.Prenotazione(data, posti, orario, telefono, username);
            if (Startup.LoggedIn == 1)
            {
                if (IdPrenotazione >= 0)
                {
                    Startup.Conferma = 1;
                    ViewBag.id = IdPrenotazione;
                }
                if (IdPrenotazione == -1)
                {
                    Startup.Conferma = 0;
                }
            }
           
            return View("Prenota");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
