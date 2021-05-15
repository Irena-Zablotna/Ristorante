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
        

        public HomeController(ILogger<HomeController> logger, RistoranteRepository ristoranteRepository )
        {
            _logger = logger;
            _ristoranteRepository = ristoranteRepository;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DoveSiamo()
        {
            return View();
        }
        public IActionResult Prova()
        {
            List<Utente> listaUtenti = _ristoranteRepository.VediUtenti();
          
            return View(listaUtenti);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
