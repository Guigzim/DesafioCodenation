using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Desafio
{
    class Challenge
    {
        public string numero_casas { get; set; }
        public string token { get; set; }
        public string cifrado { get; set; }
        public string decifrado { get; set; }
        public string resumo_criptografico { get; set; }

        public Challenge getChallenge(string url)
        {
            var request = HttpWebRequest.Create(url);
            var response = request.GetResponse();
            Stream st = response.GetResponseStream();
            StreamReader str = new StreamReader(st);

            string json = str.ReadToEnd();
            return JsonConvert.DeserializeObject<Challenge>(json);
                
        }

        public char DecryptLetter(char letra, int casas)
        {
            Dictionary<char, int> dicStrToNum = new Dictionary<char, int>();
            dicStrToNum.Add('a', 1);
            dicStrToNum.Add('b', 2);
            dicStrToNum.Add('c', 3);
            dicStrToNum.Add('d', 4);
            dicStrToNum.Add('e', 5);
            dicStrToNum.Add('f', 6);
            dicStrToNum.Add('g', 7);
            dicStrToNum.Add('h', 8);
            dicStrToNum.Add('i', 9);
            dicStrToNum.Add('j', 10);
            dicStrToNum.Add('k', 11);
            dicStrToNum.Add('l', 12);
            dicStrToNum.Add('m', 13);
            dicStrToNum.Add('n', 14);
            dicStrToNum.Add('o', 15);
            dicStrToNum.Add('p', 16);
            dicStrToNum.Add('q', 17);
            dicStrToNum.Add('r', 18);
            dicStrToNum.Add('s', 19);
            dicStrToNum.Add('t', 20);
            dicStrToNum.Add('u', 21);
            dicStrToNum.Add('v', 22);
            dicStrToNum.Add('w', 23);
            dicStrToNum.Add('x', 24);
            dicStrToNum.Add('y', 25);
            dicStrToNum.Add('z', 26);

            int num = dicStrToNum.GetValueOrDefault(letra);
            num -= casas;
            if (num < 0)
                num += 26;
            return dicStrToNum.Single(x => x.Value == num).Key;
            }
        public string DecryptString(string frase, int casas)
        {
            StringBuilder stb = new StringBuilder();
            foreach (char item in frase)
            {
                if (item == ' ')
                    stb.Append(' ');
                if (item == '.')
                    stb.Append('.');
                if (item != ' ' && item != '.')
                    stb.Append(DecryptLetter(item, casas));

            }
            return stb.ToString();
        }
        public string ToSHA1(string frase)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(frase);
            SHA1 sha = SHA1.Create();
            byte[] hash = sha.ComputeHash(buffer);
            string result = "";
            foreach (byte item in hash)
            {
                result += item.ToString("X2");
            }
            return result;


        }
    }
}
