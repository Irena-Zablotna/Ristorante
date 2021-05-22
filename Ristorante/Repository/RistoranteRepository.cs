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


        public bool IsLogged (string username, string password)
        {
            var usersList = _ristoranteContext.Utenti.ToList();
           
            if(usersList.Any(u => u.username == username &&
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

            if (usersList.Any(u => u.username == username)|| password != password1)
            {
                return false;
            }
            else if (password!=password1)
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
        

    }
}

