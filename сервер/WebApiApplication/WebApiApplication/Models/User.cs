using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApiApplication.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string? Name { get; set; }
        public string Phone { get;  set; }
        public string Password { get;  set; }
        public bool isSpecialist { get; set; }

        public User(string name, string phone, string password, bool isSpecialist)
        {
            Id = default;
            Name = name;
            Phone = phone;
            Password = password;
            this.isSpecialist = isSpecialist;
        }
    }
}
