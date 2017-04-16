using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraService.Models
{
    public class ClienteDetailDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public int FilmeId { get; set; }

        public string NomeDoFilme { get; set; }
    }
}