﻿using Ristorante.Data;
using Ristorante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Ristorante.Repository
{
    public class RistoranteRepository
    {
        private RistoranteContext _ristoranteContext;
        private readonly UserManager<IdentityUser> _userManager;

        public RistoranteRepository(RistoranteContext ristoranteContext, UserManager<IdentityUser> userManager)
        {
            _ristoranteContext = ristoranteContext;
            _userManager = userManager;
            
        }

        public List<Piatto> VediPiatti()
        {
            var result = _ristoranteContext.Piatti.ToList();
            return result;
        }



        public int Prenotazione(Prenotazione prenotazione, string username)
        {
            var users = _userManager.Users;
            var userId = (from u in users
                          where u.UserName == username
                          select u.Id).FirstOrDefault();



            //var prenotazioni = _ristoranteContext.Prenotazioni.ToList();
            int postiTotali = 40;
            int postiOccupati = _ristoranteContext.Prenotazioni.Where(x => x.orario == prenotazione.orario && x.data == prenotazione.data).ToList().Sum(x => x.numero_persone);
            bool postiLiberi = postiTotali - postiOccupati >= prenotazione.numero_persone;
            
            if (postiLiberi)
            {
                var p = new Prenotazione();
                p.id_utente = userId;
                p.numero_persone = prenotazione.numero_persone;
                p.orario = prenotazione.orario;
                p.numero_tel = prenotazione.numero_tel;
                p.data = prenotazione.data;
                _ristoranteContext.Prenotazioni.Add(p);
                _ristoranteContext.SaveChanges();

                //var aggiornato = _ristoranteContext.Prenotazioni.ToList();
                //var idPrenotazione = from a in aggiornato
                //                     where a == p
                //                     select p.id_prenotazione;
                //int result = idPrenotazione.First();
                return p.id_prenotazione;
            }
            else
            {
                return -1;
            }

        }


    }
}

