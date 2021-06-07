using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Entity_Esercizio.VievModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, RistoranteRepository ristoranteRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _ristoranteRepository = ristoranteRepository;
            _userManager = userManager;
            _signInManager = signInManager;

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
        public async Task<IActionResult> Registrazione(RegisterViewModel rvmodel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = rvmodel.Username, PasswordHash = rvmodel.Password };

                var result = await _userManager.CreateAsync(user, rvmodel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //Startup.LoggedIn = 1;
                    return RedirectToAction("Registrati");
                }
        
                 foreach (var error in result.Errors)
                 {
                        ModelState.AddModelError(string.Empty,"registrazione non riuscita");
                 }
            }
            return View ("Registrati");
        }


       
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

                ModelState.AddModelError(string.Empty, "Username o password non corretti");

            }
            return View("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
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


        //[HttpGet]
        //public IActionResult Prenotazione(DateTime data, int posti, string orario, string telefono, string username)
        //{
            
        //    int IdPrenotazione = _ristoranteRepository.Prenotazione(data, posti, orario, telefono, username);
        //    if (Startup.LoggedIn == 1)
        //    {
        //        if (IdPrenotazione >= 0)
        //        {
        //            Startup.Conferma = 1;
        //            ViewBag.id = IdPrenotazione;
        //        }
        //        if (IdPrenotazione == -1)
        //        {
        //            Startup.Conferma = 0;
        //        }
        //    }
           
        //    return View("Prenota");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
