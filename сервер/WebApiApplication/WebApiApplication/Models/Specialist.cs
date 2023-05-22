using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApplication.Models
{
    public class Specialist : User
    {
        [Column(TypeName = "nvarchar(100)")]
        public string? NameCategory { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Address { get; set; }
        public string? Description { get; set; }
        public List<Service>? Services { get; private set; }
        public Category? Category { get; set; }
        public List<WorkSchedule>? WorkSchedules { get; set; }
        public new List<Appointments>? Appointments { get; set; }
        public Specialist(string name, string phone, string password, bool isSpecialist, string nameCategory, string address) : base(name, phone, password, isSpecialist)
        {
            Name = name;
            Phone = phone;
            Password = password;
            this.isSpecialist = isSpecialist;
            NameCategory = nameCategory;
            Address = address;
            Services = new List<Service>();
        }
    }
}
