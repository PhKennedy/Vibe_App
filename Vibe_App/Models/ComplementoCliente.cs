using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe_App.Models
{
    public class ComplementoCliente
    {
        public string UrlImagem { get; set; }
        public string Empresa { get; set; }
        public Endereco Endereco { get; set; }
    }
}
