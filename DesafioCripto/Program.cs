using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;

namespace DesafioCripto
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "dfdf94e1029ffec00b5d2788196af415642b90d2";
            var requisicaoWeb = WebRequest.CreateHttp("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token="+token);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            object objResponse;

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                objResponse = reader.ReadToEnd();                
            }

            var res = JsonConvert.DeserializeObject<Infos>(objResponse.ToString());

            string txt = res.Decifrar().ToLower().TrimEnd();
            string hash = res.GetSha1(txt).ToLower();
            Console.WriteLine(txt);

            res.decifrado = txt;
            res.resumo_criptografico = hash;

            Infos.Serializar(res);


        }
    }
}
