﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ristorante.Models;
using Ristorante.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristorante.Controllers
{
    public class AdminController : Controller
    {
            private RistoranteRepository _ristoranteRepository;


        public AdminController( RistoranteRepository ristoranteRepository)
        {
           
            _ristoranteRepository = ristoranteRepository;
         

        }
        // GET: AdminController
        //[Authorize (Roles="Admin")]
        public ActionResult Adminpage()
        {
            return View();
        }

      
        public IActionResult VediMenu()
        {
            List<Piatto> listaPiatti = _ristoranteRepository.VediPiatti();
            return View(listaPiatti);
           
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            Piatto piatto = _ristoranteRepository.DettaglioPiatto(id);
            return View(piatto);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(VediMenu));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {

                return RedirectToAction(nameof(VediMenu));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
