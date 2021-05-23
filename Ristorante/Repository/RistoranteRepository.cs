using Ristorante.Data;
using Ristorante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristorante.Repository
{
    public class RistoranteRepository
    {
        private RistoranteContext _ristoranteContext;

        public RistoranteRepository(RistoranteContext ristoranteContext)
        {
            _ristoranteContext = ristoranteContext;
        }

        public List<Piatto> VediPiatti()
        {
            var result = _ristoranteContext.Piatti.ToList();
            return result;
        }


        public bool IsLogged(string username, string password)
        {
            var usersList = _ristoranteContext.Utenti.ToList();

            if (usersList.Any(u => u.username == username &&
             u.password == password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Registered(string username, string password, string password1)
        {
            var usersList = _ristoranteContext.Utenti.ToList();

            if (usersList.Any(u => u.username == username) || password != password1)
            {
                return false;
            }

            else
            {
                var newUser = new Utente();
                newUser.username = username;
                newUser.password = password;
                _ristoranteContext.Add(newUser);
                _ristoranteContext.SaveChanges();
                return true;
            }
        }


        public int Prenotazione(DateTime data, int posti, string orario, string telefono, string username)
        {
            var usersList = _ristoranteContext.Utenti.ToList();

            var prenotazioni = _ristoranteContext.Prenotazioni.ToList();

            //int postiPranzo = (from p in prenotazioni where p.orario == "pranzo" && p.data == DateTime.Today select p.numero_persone).Sum();

            //int postiCena = (from p in prenotazioni where p.orario == "cena" && p.data == DateTime.Today select p.numero_persone).Sum();

           

            //if (orario == "pranzo" && postiPranzo > posti && usersList.Any(u => u.username == username))
            //{
                var utenteScelto = from u in usersList
                                   where u.username == username
                                   select u.id_utente;
                var p = new Prenotazione();
                p.numero_persone = posti;
                p.orario = orario;
                p.numero_tel = telefono;
                p.data = data;
                p.id_utente = utenteScelto.First();
                _ristoranteContext.Prenotazioni.Add(p);
                _ristoranteContext.SaveChanges();
            //}
            var aggiornato = _ristoranteContext.Prenotazioni.ToList();
            var idPrenotazione = from a in aggiornato
                                 where a == p
                                 select p.id_prenotazione;
            int result = idPrenotazione.First();

            return result;
        }

        
    }
}

