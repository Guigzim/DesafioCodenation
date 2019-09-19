using System;
using System.IO;

namespace Desafio
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "9cef946a82be7d054537cb1f466dcdaca4c6a808";
            string urlGet = $"https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token={token}";
            string urlPost = $"https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token={token}";

            Challenge ch = new Challenge().getChallenge(urlGet);
            ch.decifrado = ch.DecryptString(ch.cifrado, int.Parse(ch.numero_casas));
            ch.resumo_criptografico = ch.ToSHA1(ch.decifrado);
            Console.WriteLine(ch.decifrado);

            FileStream file = new FileStream("answer.json", FileMode.OpenOrCreate);
            
            
            

        }
    }
}
