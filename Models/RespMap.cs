using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AireGo.Models
{
    [Serializable]
    public class RespMap
    {
        [JsonPropertyName(name: "code")]
        public int Code { get; set; }
        [JsonPropertyName(name: "message")]
        public string Message { get; set; }
        [JsonPropertyName(name: "data")]
        public object Data { get; set; }
    }
}
