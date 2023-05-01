using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace graduate_work.Models
{
    [Serializable]
    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nameServices")]
        public List<NameService> nameServices { get; set; } = null;

        [JsonPropertyName("specialist")]
        public List<Specialist> Specialist { get; set; }
    }
}
