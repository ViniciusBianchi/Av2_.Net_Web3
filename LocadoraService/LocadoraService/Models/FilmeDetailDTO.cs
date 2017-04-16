using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraService.Models
{
    public class FilmeDetailDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Categoria { get; set; }

        public int FaixaEtaria { get; set; }

        public decimal Preco { get; set; }
    }
}