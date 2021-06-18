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
//----------------------------------------REGISTRATI VISTA--------------
       
        public IActionResult Registrati()
        {
            return View();
        }
//----------------------------------------REGISTRAZIONE UTENTE----------

        [HttpPost]
        public async Task<IActionResult> Registrazione(RegisterViewModel rvmodel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = rvmodel.Username, PasswordHash = rvmodel.Password };

                var result = await _userManager.CreateAsync(user, rvmodel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Registrati");
                }
                
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }
            
            return View("Registrati");
        }

//------------------------------------LOGIN UTENTE-----------------------
       
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvmodel)
        {
            var user = await _userManager.FindByNameAsync(lvmodel.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(lvmodel.Username, lvmodel.Password, isPersistent: false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                
               ModelState.AddModelError("","Dati non corretti" );

            }
            Startup.Conferma = 3;
           
            return View("Index");
        }
//-----------------------------------LOGOUT UTENTE-----------------------------------

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
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
      //[Authorize]
        public IActionResult Prenotazione (Prenotazione prenotazione, string username) 
        {

            int IdPrenotazione = _ristoranteRepository.Prenotazione(prenotazione, username);
            if (_signInManager.IsSignedIn(User))
            {
                if (IdPrenotazione >= 0)
                {
                    Startup.Conferma = 1;
                    TempData["id"] = IdPrenotazione;
                }
                if (IdPrenotazione == -1)
                {
                    Startup.Conferma = -1;
                }
                return RedirectToAction("Prenota");
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
            
            
            return View();
        }


        //---------------------------------VISUALIZZA PRENOTAZIONE---------------
       
        //[Authorize]
        public IActionResult VediPrenotazione (int id)
        {
            var tuaPrenotazione = _ristoranteRepository.VisualizzaPrenotazione(id);
            if (tuaPrenotazione != null)
            {
               
                return View(tuaPrenotazione);
            }

            return RedirectToAction("Prenota");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
