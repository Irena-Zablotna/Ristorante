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
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;

        }


        //--------------VISTA REGISTRATI--------------

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
        //----------------VISTA LOGIN UTENTE-------------------
         

        public IActionResult Login()
        {
            return View();
        }
        //------------------------------------LOGIN UTENTE-----------------------

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvmodel)
        {
            if (ModelState.IsValid) 
            { 
            var user = await _userManager.FindByNameAsync(lvmodel.Username);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(lvmodel.Username, lvmodel.Password, isPersistent: false, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Dati non corretti");
                }
            }
       
            return View(lvmodel);
        }
        //-----------------------------------LOGOUT UTENTE-----------------------------------

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
