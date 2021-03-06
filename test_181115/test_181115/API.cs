﻿using Newtonsoft.Json;
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
        private string defaultUrl = "https://student.sps-prosek.cz/~bounlfi15/evidence/test.php";
        private string tablPrefix = "";

        public async Task<string> GetPostsJsonTask(string tbl, string id = "")
        {
            var query = "";
            if (int.TryParse(id, out int itemID))
            {
                query = "&id=" + itemID;
            }

            var uri = new Uri(defaultUrl + "?tbl=" + tablPrefix + tbl + query);

            string content = await Task.Run(async () =>
            {
                var response = await client.GetAsync(uri);
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

        public async Task<List<Item>> ParsePostJsonTask(string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Item>>(json));
        }
    }
}
