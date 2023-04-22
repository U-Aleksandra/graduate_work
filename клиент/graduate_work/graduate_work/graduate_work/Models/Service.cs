using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace graduate_work.Models
{
    public class Service
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool StartPrice { get; set; }
        public DateTime ServicesTime { get; set; }
        public DateTime Break { get; set; }

        [JsonPropertyName("nameService"), JsonInclude]
        public NameService NameService { get; set; } = null;

        [JsonPropertyName("specialist"), JsonInclude]
        public Specialist Specialist { get; set; } = null;
    }
}
