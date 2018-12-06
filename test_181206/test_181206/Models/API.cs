using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test_181206.Models
{
    public class API
    {
        private HttpClient client = new HttpClient();
        private string defaultUrl = "https://student.sps-prosek.cz/~bounlfi15/evidence/api.php";
        private string tablPrefix = "ObS";

        public async Task<string> GetAllJsonTask(string tbl, int id = -1)
        {
            var stringID = "";
            if (id != -1)
            {
                stringID = "&id=" + id;
            }

            var uri = new Uri(defaultUrl + "?tbl=" + tablPrefix + tbl + stringID);

            string content = await Task.Run(async () =>
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });
            return content;
        }

        public async void InsertData(List<KeyValuePair<string, string>> keyValues, string tbl)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, defaultUrl + "?tbl=" + tablPrefix + tbl);

            request.Content = new FormUrlEncodedContent(keyValues);

            string content = await Task.Run(async () =>
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });
        }

        public async Task<List<Item>> ParseJsonTask(string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Item>>(json));
        }
    }
}
