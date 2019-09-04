using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.Models.ViewModel
{
    public class OrderApiResponse<T>
        where T:class
    {
        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
