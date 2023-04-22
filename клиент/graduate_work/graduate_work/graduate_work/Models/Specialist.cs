﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Xamarin.Essentials.Permissions;

namespace graduate_work.Models
{
    [Serializable]
    public class Specialist : User
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("services"), JsonInclude]
        public List<Service> Services { get; private set; }

        [System.Text.Json.Serialization.JsonConstructor]
        public Specialist(string name, string phone, string password, bool isSpecialist, string category, string address, string description = "Напишите несколько слов о себе") : base(name, phone, password, isSpecialist)
        {
            Name = name;
            Phone = phone;
            Password = password;
            this.isSpecialist = isSpecialist;
            Category = category;
            Address = address;
            Description = description;
            Services = new List<Service>();
        }
    }

    
}
