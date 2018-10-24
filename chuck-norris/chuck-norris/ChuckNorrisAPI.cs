using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace chuck_norris
{
    public class ChuckNorrisAPI
    {
        public async Task<string> GetPostsJsonTask(string query = "dev")
        {
            var client = new HttpClient();
            var uri = new Uri("https://api.chucknorris.io/jokes/random?category=" + query);

            string content = await Task.Run(async () =>
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });

            return content;
        }

        public async Task<Joke> ParsePostJsonTask(string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<Joke>(json));

        }
    }
}
