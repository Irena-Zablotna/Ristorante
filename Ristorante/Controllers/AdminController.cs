using Microsoft.AspNetCore.Authorization;
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
            private IRistoranteRepository _ristoranteRepository;


        public AdminController( IRistoranteRepository ristoranteRepository)
        {
           
            _ristoranteRepository = ristoranteRepository;
         

        }
        // GET: AdminController
       
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
        public ActionResult Create(Piatto piatto)
        {
            Piatto p = _ristoranteRepository.CreaPiatto(piatto);
            
            
                if(ModelState.IsValid && p!=null)
                {
                int id = p.id;

                    ViewData["creato"] = $"Il piatto {id} è stato aggiunto al menu";
                return View ("Details", p);
                }
          
                return View("Create");
           
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            Piatto piatto = _ristoranteRepository.DettaglioPiatto(id);
            return View(piatto);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Piatto piatto)
        {

            if (id != piatto.id)
            {
                return NotFound();
            }

            bool updated = _ristoranteRepository.ModificaAdmin(piatto, id);

                if (updated)
                {
                ViewData["conferma"] = $"Il piatto {id} è stato modificato";
                    return View("Details", piatto);
                }

            return View ("Edit");
        }

        // GET: AdminController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Piatto piatto = _ristoranteRepository.DettaglioPiatto(id);
            return View(piatto);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDish(int id)
        {
            bool deleted = _ristoranteRepository.CancellaAdmin(id);
            if (deleted)
            {
                ViewData["cancellato"] = $"il piatto {id} è stato rimosso dal menu";
                return View("Adminpage");
            }
            ViewData["impossibile"] = $"non è stato possibile cancellare il piatto n.{id}, riprova";
            return View("Adminpage");
        }
    }
}
