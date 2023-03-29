using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace graduate_work.Models
{
    [Serializable]
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("isSpecialist")]
        public bool isSpecialist { get; set; }

        [System.Text.Json.Serialization.JsonConstructor]
        public User(int id, string name, string phone, string password, bool isSpecialist)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Password = password;
            this.isSpecialist = isSpecialist;
        }
        public User(string name, string phoneNumber, string password, bool isSpecialist)
        {
            
            Name = name;
            Phone = phoneNumber;
            Password = password;
            this.isSpecialist = isSpecialist;
        }

        public User(string phoneNumber, string password)
        {
            Phone = phoneNumber;
            Password = password;
        }

    }
}
