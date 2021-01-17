using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AireGo.Models
{
    [Table(Name = "T_User")]
    [Serializable]
    public class UserMap
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        [JsonPropertyName(name: "id")]
        public int Id { get; set; }
        [JsonPropertyName(name: "name")]
        public string Name { get; set; }
        [JsonIgnore]
        public string Passwd { get; set; }
        [JsonPropertyName(name: "email")]
        public string Email { get; set; }
    }
}
