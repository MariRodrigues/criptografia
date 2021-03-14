using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace DesafioCripto
{
    class Infos
    {
        public int numero_casas { get; set; }
        public String token { get; set; }
        public String cifrado { get; set; }
        public String decifrado { get; set; }
        public String resumo_criptografico { get; set; }

        public string Decifrar()
        {
            string[] palavra = cifrado.ToLower().Split(' ');

            List<char> traduzido = new List<char>();

            char[] letra;
            int casas = numero_casas;
            int limite_min = 97; // Letra "a" na tabela ASCII
            int limite_max = 122; // Letra "z" na tabela ASCII
            int x, r, NovoAscii;
            int minimo_ascii = casas + limite_min;

            for (int i = 0; i < palavra.Length; i++)
            {
                string varLocal = palavra[i];
                letra = varLocal.ToCharArray();

                for (int j = 0; j < letra.Length; j++)
                {
                    char word = letra[j];
                    int ascii = Convert.ToInt32(word);
                    if (ascii == 46 || ascii > 48 && ascii <57) // Código do ponto final e números não irão alterar
                    {
                        NovoAscii = ascii;
                    }
                    else if (ascii < minimo_ascii) //
                    {
                        x = ascii - limite_min;
                        r = casas - x;
                        NovoAscii = limite_max - (r - 1);
                    }
                    else
                    {
                        NovoAscii = ascii - casas;
                    }
                    char word2 = Convert.ToChar(NovoAscii);
                    traduzido.Add(word2);
                }

                traduzido.Add(' ');
            }
            var myString = new string(traduzido.ToArray());

            return myString;
        }

        public static void Serializar(Object infos)
        {
            using (var streamWriter = new System.IO.StreamWriter("answer.json"))
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(infos);
                streamWriter.Write(json);
            }
        }

        public string GetSha1(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var hashData = new SHA1Managed().ComputeHash(data);
            var hash = string.Empty;
            foreach (var b in hashData)
            {
                hash += b.ToString("X2");
            }
            return hash;
        }
    }
}
