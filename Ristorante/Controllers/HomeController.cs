﻿using System;
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

            if(userRegistered==true)
            {
                Startup.LoggedIn = userRegistered;
                Startup.Username = username;
            }
            return View("Registrati");
        }



        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            bool userLogged = _ristoranteRepository.IsLogged(username, password);
            Startup.LoggedIn = userLogged;
            Startup.Username = username;
            return View("Index");
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        public IActionResult Piatti()
        {
            ViewBag.LoggedIn = false;

            List<Piatto> listaPiatti = _ristoranteRepository.VediPiatti();
            return View(listaPiatti);
        }
        public IActionResult Prenota()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
