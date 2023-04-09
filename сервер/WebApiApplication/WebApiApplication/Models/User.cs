using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApiApplication.Models
{
    public class User
    {
        public int Id { get;  set; }
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

        public override bool Equals(Object obj)
        {
            if (obj is Specialist specialist)
            {
                foreach (var prop in typeof(Specialist).GetProperties())
                {
                    Console.WriteLine(prop.GetValue(specialist).ToString()+" - "+ prop.GetValue(this).ToString());
                    if (prop.GetValue(specialist) != prop.GetValue(this)) return false;
                }
            }
            else if (obj is User user)
            {
                foreach (var prop in typeof(User).GetProperties())
                {
                    Console.WriteLine(prop.GetValue(user).ToString() + " - " + prop.GetValue(this).ToString());
                    if (prop.GetValue(user).Equals(prop.GetValue(this))) return false;
                }
            }
            return true;
        }
    }
}
