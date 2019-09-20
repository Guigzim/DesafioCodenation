using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;


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

            FileStream arquivo = new FileStream("answer.json", FileMode.OpenOrCreate);
            byte[] ab = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ch));
            arquivo.Write(ab);


            try
            {
                StreamContent stcontent = new StreamContent(arquivo);
                
                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(stcontent, "answer", "answer.json");
                
                
                //stcontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");
                //stcontent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "answer", FileName = "answer" };
                //form.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");
                ///form.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
                
                HttpClient client = new HttpClient();
                
                var response = client.PostAsync(urlPost, form);
                Console.WriteLine($"StatusCode: {response.Result.StatusCode}");
                arquivo.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }
    }
}
