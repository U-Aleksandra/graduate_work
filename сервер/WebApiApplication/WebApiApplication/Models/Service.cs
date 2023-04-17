using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApplication.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(20, 0)")]
        public decimal Price { get; set; }
        public bool StartPrice { get; set; }
        public DateTime ServicesTime { get; set; }
        public DateTime Break { get; set; }
        public NameService? NameService { get; private set; }
        public Specialist? Specialist { get; private set; }
    }
}
