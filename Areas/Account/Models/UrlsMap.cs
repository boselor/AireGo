using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AireGo.Areas.Account.Models
{
    [Table(Name = "T_Urls")]
    [Serializable]
    public class UrlsMap
    {
        [JsonPropertyName(name: "id")]
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }
        [JsonPropertyName(name: "tag")]
        public string Tag { get; set; }
        [JsonPropertyName(name: "time")]
        public string Posttime { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");
        [JsonPropertyName(name: "title")]
        public string Title { get; set; }
        [JsonPropertyName(name: "url")]
        public string Url { get; set; }
        [JsonPropertyName(name: "desc")]
        public string Description { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
