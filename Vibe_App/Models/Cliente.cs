using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe_App.Models
{
     public class Cliente
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Especial { get; set; }
        public ComplementoCliente Complemento { get; set; }
    }
}
