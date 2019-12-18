using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Vibe_App.Services
{
    public class Criptografia
    {
        public string CriptografarMD5(string entrada)
        {
            using (MD5 hashMD5 = MD5.Create())
            {
                byte[] dado = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(entrada));
                StringBuilder StringBuilder = new StringBuilder();

                for (int i = 0; i < dado.Length; i++)
                {
                    StringBuilder.Append(dado[i].ToString("X2"));
                }

                return StringBuilder.ToString();
            }
        }
    }
}
