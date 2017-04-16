using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraService.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }
        // Foreign key
        public int FilmeId { get; set; }
        // Navigation property
        public Filme Filme { get; set; }
    }
}