using Ristorante.Data;
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
//-----------------------------------------------------------

        public List<Piatto> VediPiatti()
        {
            var result = _ristoranteContext.Piatti.ToList();
            return result;
        }


//-----------------------------------------------------------------------------

        public int Prenotazione(Prenotazione prenotazione, string username)
        {
            var users = _userManager.Users;
            var userId = (from u in users
                          where u.UserName == username
                          select u.Id).FirstOrDefault();
   
            int postiTotali = 40;
            int postiOccupati = _ristoranteContext.Prenotazioni.Where(x => x.orario == prenotazione.orario && x.data == prenotazione.data).ToList().Sum(x => x.numero_persone);
            bool postiLiberi = postiTotali - postiOccupati >= prenotazione.numero_persone;
            
            if (postiLiberi && userId!=null)
            {
                var p = new Prenotazione();
                p.id_utente = userId;
                p.numero_persone = prenotazione.numero_persone;
                p.orario = prenotazione.orario;
                p.numero_tel = prenotazione.numero_tel;
                p.data = prenotazione.data;
                _ristoranteContext.Prenotazioni.Add(p);
                _ristoranteContext.SaveChanges();

                return p.id_prenotazione;
            }
            else
            {
                return -1;
            }

        }

        //--------------------------------------------------

        public Prenotazione  VisualizzaPrenotazione (int id)
        {
           var prenotazioni = _ristoranteContext.Prenotazioni;
          var result = (from p in prenotazioni
                                where p.id_prenotazione==id
                                select p).FirstOrDefault();

            if (result != null)
            {
                
                 return result;
            }

            return null;
        }

//-----------------------------------------------------

        public bool CancellaPrenotazione(int id)
        {
            var prenotazioni = _ristoranteContext.Prenotazioni;
            var result = (from p in prenotazioni
                          where p.id_prenotazione == id
                          select p).FirstOrDefault();

            if (result != null)
            {
                _ristoranteContext.Remove(result);
                _ristoranteContext.SaveChanges();
                return true;

            }
            return false;  

        }
    }
}

