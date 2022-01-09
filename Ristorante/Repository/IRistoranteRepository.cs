using Ristorante.Models;
using System.Collections.Generic;

namespace Ristorante.Repository
{
    public interface IRistoranteRepository
    {
        
        public List<Piatto> VediPiatti();
        public int Prenotazione(Prenotazione prenotazione, string username);
        public Prenotazione VisualizzaPrenotazione(int id, string username);
        public bool CancellaPrenotazione(int id);

        public bool Modifica(Prenotazione prenotazione, int id);

        public Piatto DettaglioPiatto(int id);
        public bool ModificaAdmin(Piatto piatto, int id);
        public Piatto CreaPiatto(Piatto piatto);
        public bool CancellaAdmin(int id);

    }
}
