// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Ristorante.Models
{
    public partial class Tipo_Piatto
    {
        public Tipo_Piatto()
        {
            Piatti = new HashSet<Piatto>();
        }

        public string Tipo_piatto1 { get; set; }
        public string Descrizione { get; set; }

        public virtual ICollection<Piatto> Piatti { get; set; }
    }
}