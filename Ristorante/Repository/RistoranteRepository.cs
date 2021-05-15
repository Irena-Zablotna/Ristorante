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

        public List<Utente> VediUtenti()
        {
            var result = _ristoranteContext.Utenti.ToList();
            return result;
        }
        }
    }

