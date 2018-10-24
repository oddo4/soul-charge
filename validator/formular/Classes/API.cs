using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace formular.Classes
{
    class API
    {
        public async Task<string> GetPostsJsonTask(string query)
        {
            var client = new HttpClient();
            var uri = new Uri("https://api.edamam.com/search?q=" + query + "&app_id=" + apiID + "&app_key=" + apiKey + "&from=0&to=10");

            string content = await Task.Run(async () =>
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });
            return content;
        }
        public async Task<ResultData> ParsePostJsonTask(string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<ResultData>(json));

        }
    }
}
