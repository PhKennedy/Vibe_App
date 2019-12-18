using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe_App.Models
{
    public class User
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public DateTime Nascimento { get; set; }
    }
}
