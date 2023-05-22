using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApplication.Models
{
    public class User
    {
        [Key]
        public int Id { get;  set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Name { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string Phone { get;  set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Password { get;  set; }
        public bool isSpecialist { get; set; }
        public List<Appointments>? Appointments { get; set; }   
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
