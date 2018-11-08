using Newtonsoft.Json;
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

        private HttpClient client = new HttpClient();

        public async Task<string> GetPostsJsonTask(string query)
        {
            var uri = new Uri("https://student.sps-prosek.cz/~bounlfi15/evidence/api.php");

            string content = await Task.Run(async () =>
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });
            return content;
        }

        public async void InsertData(Person newPerson)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://student.sps-prosek.cz/~bounlfi15/evidence/api.php");

            var keyValues = new List<KeyValuePair<string, string>>();

            keyValues = newPerson.CreateKeyValues();

            request.Content = new FormUrlEncodedContent(keyValues);

            string content = await Task.Run(async () =>
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });      
        }


        public async Task<List<Person>> ParsePostJsonTask(string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Person>>(json));

        }
    }
}
