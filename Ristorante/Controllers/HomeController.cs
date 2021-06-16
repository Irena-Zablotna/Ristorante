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
                    return RedirectToAction("Registrati");
                }
                
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                 
            }
            
            return View("Registrati");
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
                
               ModelState.AddModelError(string.Empty,"Dati non corretti" );

            }
            Startup.Conferma = 3;
            return Redirect("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Piatti()
        {

           List<Piatto> listaPiatti = _ristoranteRepository.VediPiatti();
            return View(listaPiatti);
        }


        public IActionResult Prenota()
        {

            return View();
        }


        [HttpPost]
      //[Authorize]
        public IActionResult Prenotazione (Prenotazione prenotazione, string username) 
        {

            int IdPrenotazione = _ristoranteRepository.Prenotazione(prenotazione, username);
            if (_signInManager.IsSignedIn(User)&& User.Identity.Name==username)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
