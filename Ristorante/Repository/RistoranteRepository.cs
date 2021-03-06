using Ristorante.Data;
using Ristorante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Ristorante.Repository
{
    public class RistoranteRepository:IRistoranteRepository
    {
        private RistoranteContext _ristoranteContext;
        private readonly UserManager<IdentityUser> _userManager;

        public RistoranteRepository(RistoranteContext ristoranteContext, UserManager<IdentityUser> userManager)
        {
            _ristoranteContext = ristoranteContext;
            _userManager = userManager;
            
        }
//-------------------------------VEDI PIATTI ----------------------------

        public List<Piatto> VediPiatti()
        {
            var result = _ristoranteContext.Piatti.ToList();
            return result;
        }


//--------------------------------PRENOTA---------------------------------------------

        public int Prenotazione(Prenotazione prenotazione, string username)
        {
            var users = _userManager.Users;
            var userId = _userManager.Users.Where(u => u.UserName == username)
               .Select(u => u.Id).FirstOrDefault();

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

        //-----------------------------VISUALIZZA PRENOTAZIONE---------------------

        public Prenotazione  VisualizzaPrenotazione (int id, string username)
        {

            var userId = _userManager.Users.Where(u => u.UserName == username)
                .Select(u => u.Id).FirstOrDefault();
          
            var result = _ristoranteContext.Prenotazioni.Where(p => p.id_prenotazione ==id)
                .FirstOrDefault();
               
           

            if (result != null && result.id_utente == userId)
            {
                
                 return result;
            }

            return null;
        }

//-------------------------------CANCELLA PRENOTAZIONE----------------------

        public bool CancellaPrenotazione(int id)
        {
            var result = _ristoranteContext.Prenotazioni.Where(p => p.id_prenotazione == id)
                .FirstOrDefault();

            if (result != null)
            {
                _ristoranteContext.Remove(result);
                _ristoranteContext.SaveChanges();
                return true;

            }
            return false;  

        }
//-----------------MODIFICA PRENOTAZIONE-----------------

        public bool Modifica (Prenotazione prenotazione, int id)
        {
            var result = _ristoranteContext.Prenotazioni.Where(p => p.id_prenotazione == id)
               .FirstOrDefault();

            if (result != null)
            {
                result.data = prenotazione.data;
               
                if (prenotazione.numero_persone == 0)
                { prenotazione.numero_persone=result.numero_persone; }

                else { result.numero_persone = prenotazione.numero_persone; }

                result.numero_tel = prenotazione.numero_tel;

                if (prenotazione.orario == null)
                { prenotazione.orario = result.orario; }

                else { result.orario = prenotazione.orario; }

                _ristoranteContext.Update(result);
                _ristoranteContext.SaveChanges();
                return true;

            }

            return false;
        }

        //------------VEDI Piatto Admin------------------------------------

        public Piatto DettaglioPiatto(int id)
        {
            Piatto piatto = _ristoranteContext.Piatti.Where(p => p.id == id).FirstOrDefault();
            if(piatto!=null)
            {
                return piatto;
            }
            return null;
        }
        //--------------AGGIORNA PIATTO Admin-------------------------

        public bool ModificaAdmin(Piatto piatto, int id)
        {
            var result = _ristoranteContext.Piatti.Where(p => p.id == id).FirstOrDefault();
            if (piatto != null)
            {
                result.Nome = piatto.Nome;
                result.Prezzo = piatto.Prezzo;
                if (piatto.Tipo_piatto == null)
                {
                    piatto.Tipo_piatto = result.Tipo_piatto;
                }
                else
                {
                    result.Tipo_piatto = piatto.Tipo_piatto;
                }
                _ristoranteContext.Piatti.Update(result);
                _ristoranteContext.SaveChanges();
                return true;
            }
            return false;
        }

        //------------INSERISCI NUOVO PIATTO Admin----------------------------

        public Piatto CreaPiatto(Piatto piatto)
        {
            var p = new Piatto();
            p.Nome = piatto.Nome;
            p.Prezzo = piatto.Prezzo;
            p.Tipo_piatto = piatto.Tipo_piatto;

            if (p != null)
            {
                _ristoranteContext.Piatti.Add(p);
                _ristoranteContext.SaveChanges();
                return p;
            }
            return null; 
        }

        //-----------CANCELLA PIATTO Admin------------

        public bool CancellaAdmin (int id)
        {
            var result = _ristoranteContext.Piatti.Where(p => p.id == id).FirstOrDefault();
            if (result != null)
            {
                _ristoranteContext.Piatti.Remove(result);
                _ristoranteContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

