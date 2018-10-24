using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuck_norris
{
    public partial class Joke
    {
        [JsonProperty("category")]
        public string[] Category { get; set; }

        [JsonProperty("icon_url")]
        public Uri IconUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
