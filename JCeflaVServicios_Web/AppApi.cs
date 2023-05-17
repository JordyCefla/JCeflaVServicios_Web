using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JCeflaVServicios_Web.Models;
using Newtonsoft.Json;

namespace JCeflaVServicios_Web
{
    public class AppApi
    {
        const String URL = "http://186.5.86.118/AppEcuador/post.php";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "Application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");
            return client;
        }
        
        public async Task<List<Datos>> GetHighScores()
        {
            HttpClient client = GetClient();
            //HttpResponseMessage response = await client.GetAsync(dados);
            var response = await client.GetAsync(URL);
            if (response.IsSuccessStatusCode) //codigo 200
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Datos>>(content);
            }
            return new List<Datos>();
        }

        public async Task<Datos> GetHighScore(int Id)
        {
            String dados = URL + "?id=" + Id;
            //Uri uri = new Uri(dados);
            HttpClient client = GetClient();
            HttpResponseMessage response = await client.GetAsync(dados);
            //var response = await client.GetAsync(dados);
            if (response.IsSuccessStatusCode) //codigo 200
            {
                string content = await response.Content.ReadAsStringAsync();
                var games = JsonConvert.DeserializeObject<List<Datos>>(content);
                return games[0];
            }
            return new Datos();
        }

        public async Task CreateHighScore(Datos game)
        {
            String dados = URL;
            string json = JsonConvert.SerializeObject(game);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(dados, content);
        }

        public async Task UpDateHighScore(Datos game)
        {
            String dados = URL + "/" + game.id;
            string json = JsonConvert.SerializeObject(game);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(dados, content);
        }

        public async Task DeleteHighScore(int Id)
        {
            String dados = URL + "/" + Id.ToString();
            HttpClient client = GetClient();
            HttpResponseMessage response = await client.DeleteAsync(dados);
        }
    }
}
