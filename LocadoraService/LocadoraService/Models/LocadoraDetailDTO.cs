using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraService.Models
{
    public class LocadoraDetailDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        // Navigation property
        public IList<Filme> Filmes { get; set; }

    }
}