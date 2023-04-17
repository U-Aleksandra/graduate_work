using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApiApplication.Models
{
    public class Specialist : User
    {
        public string Category { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public List<Service> Services { get; private set; }

        public Specialist(string name, string phone, string password, bool isSpecialist, string category, string address) : base(name, phone, password, isSpecialist)
        {
            Name = name;
            Phone = phone;
            Password = password;
            this.isSpecialist = isSpecialist; 
            Category = category;
            Address = address;
        }
    }
}
