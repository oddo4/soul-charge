using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test_181115
{
    public class API
    {
        private HttpClient client = new HttpClient();

        public async Task<string> GetPostsJsonTask(string id)
        {
            var query = "";
            if (int.TryParse(id, out int itemID))
            {
                query = "?id=" + itemID;
            }

            var uri = new Uri("https://student.sps-prosek.cz/~bounlfi15/evidence/test.php" + query);

            string content = await Task.Run(async () =>
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });
            return content;
        }

        public async Task<List<Item>> ParsePostJsonTask(string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Item>>(json));
        }
    }
}
