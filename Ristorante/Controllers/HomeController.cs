using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ristorante.VievModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ristorante.Data;
using Ristorante.Models;
using Ristorante.Repository;
using Microsoft.AspNetCore.Authorization;


namespace Ristorante.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RistoranteRepository _ristoranteRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, RistoranteRepository ristoranteRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _ristoranteRepository = ristoranteRepository;
            _userManager = userManager;
            _signInManager = signInManager;

        }
//------------------------------------------HOME PAGE------------------
        public IActionResult Index()
        {
            return View();
        }
       

        //-------------------------------------PIATTI VISTA------------------------------
        public IActionResult Piatti()
        {

           List<Piatto> listaPiatti = _ristoranteRepository.VediPiatti();
            return View(listaPiatti);
        }
//-----------------------------------PRENOTA VISTA-----------------------------------
    
        public IActionResult Prenota()
        {

            return View();
        }

//-----------------------------------PRENOTAZIONE UTENTE----------------------------
        [HttpPost]
     
        public IActionResult Prenotazione (Prenotazione prenotazione, string username) 
        {

            int IdPrenotazione = _ristoranteRepository.Prenotazione(prenotazione, username);
            if (_signInManager.IsSignedIn(User)&& ModelState.IsValid)
            {
                if (IdPrenotazione >= 0)
                {
                    ViewData["num"] = $"il tuo numero di prenotazione è: {IdPrenotazione}";
                }
                if (IdPrenotazione == -1)
                {
                    ViewData["noPosti"] = "Purtroppo non abbiamo più posti disponibili per la data che hai scelto";
                }
                return View("Prenota");
            }

            else
            {
               return View("Registrati");
            }
          
        }

//------------------------------CANCELLAZIONE PRENOTAZIONE-------------------------
        [HttpPost]

        public IActionResult Cancella(int id )
        {
           if (_signInManager.IsSignedIn(User))
            {
                bool fatto = _ristoranteRepository.CancellaPrenotazione(id);
                if (fatto)
                {
                    
                    ViewData["numCanc"] = $"la prenotazione {id} è stata cancellata";
                    return View ("Prenota");
                }

            }

            ViewData["impossibile"] = $"Non è stato possibile cancellare la prenotazione n. {id}, riprova";

            return View("Prenota");
        }


        //---------------------------------VISUALIZZA PRENOTAZIONE---------------

        //[Authorize]
        [HttpPost]
        public IActionResult VediPrenotazione (int id, string username)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var tuaPrenotazione = _ristoranteRepository.VisualizzaPrenotazione(id, username);
                if (tuaPrenotazione != null)
                {

                    ViewBag.id = id;
                    return View(tuaPrenotazione);
                }
            }

            ViewData["nonTrovata"] = $"prenotazione {id} non trovata";
            return View("Prenota");
        }
        //--------------------------------MODIFICA PRENOTAZIONE---------------
        [HttpPost]
        public IActionResult Modifica( Prenotazione prenotazione, int id)
        {
             if (_signInManager.IsSignedIn(User))
            {
                bool modificata = _ristoranteRepository.Modifica(prenotazione, id);

                if (modificata)
                {
                    ViewData["modifica"] = $"la tua prenotazione {id} è stata modificata";
                    return View("Prenota");
                }
            }
                   
            return View("VediPrenotazione");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
