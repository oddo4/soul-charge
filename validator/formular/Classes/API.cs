using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace formular.Classes
{
    class API
    {
        private HttpClient client = new HttpClient();
        private string defaultUrl = "https://student.sps-prosek.cz/~bounlfi15/evidence/api.php";
        private string tablPrefix = "ObS";

        public async Task<string> GetData(string tbl, string args = "")
        {
            var uri = new Uri(defaultUrl + "?tbl=" + tablPrefix + tbl + "&" + args);

            string content = await Task.Run(async () =>
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });
            return content;
        }

        //public async Task<string> GetItemsAllJsonTask()
        //{
        //    var uri = new Uri(defaultUrl + "?tbl=ObSItem");

        //    string content = await Task.Run(async () =>
        //    {
        //        var response = await client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
        //        throw new HttpRequestException("Communication with server failed");
        //    });
        //    return content;
        //}

        //public async void InsertPersonData(Person newPerson)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Post, defaultUrl + "?tbl=" + tablPrefix + "Person");

        //    var keyValues = new List<KeyValuePair<string, string>>();

        //    keyValues = newPerson.CreateKeyValues();

        //    request.Content = new FormUrlEncodedContent(keyValues);

        //    string content = await Task.Run(async () =>
        //    {
        //        var response = await client.SendAsync(request);
        //        if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
        //        throw new HttpRequestException("Communication with server failed");
        //    });
        //}

        public async Task<string> GetPostData(List<KeyValuePair<string, string>> keyValues, string tbl)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, defaultUrl + "?tbl=" + tablPrefix + tbl);

            request.Content = new FormUrlEncodedContent(keyValues);

            string content = await Task.Run(async () =>
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });

            return content;
        }

        public async Task<bool> InsertData(List<KeyValuePair<string, string>> keyValues, string tbl)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, defaultUrl + "?tbl=" + tablPrefix + tbl);

            request.Content = new FormUrlEncodedContent(keyValues);

            string content = await Task.Run(async () =>
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Communication with server failed");
            });

            return true;
        }
        //public async void InsertOrderData(Person newPerson)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Post, "https://student.sps-prosek.cz/~bounlfi15/evidence/api.php");

        //    var keyValues = new List<KeyValuePair<string, string>>();

        //    keyValues = newPerson.CreateKeyValues();

        //    request.Content = new FormUrlEncodedContent(keyValues);

        //    string content = await Task.Run(async () =>
        //    {
        //        var response = await client.SendAsync(request);
        //        if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
        //        throw new HttpRequestException("Communication with server failed");
        //    });
        //}


        public async Task<List<T>> ParseJsonTask<T>(string json) where T : class
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(json));
        }

        //public async Task<List<Item>> ParseJsonTask(string json)
        //{
        //    return await Task.Run(() => JsonConvert.DeserializeObject<List<Item>>(json));
        //}

        public async Task<List<Order>> ParseOrderJsonTask(string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Order>>(json));
        }
    }
}
