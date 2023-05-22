using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApplication.Models
{
    public class Appointments
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateApointment {get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Note { get; set; }
        public Specialist? Specialist { get; set; }
        public User? User { get; set; }
    }
}
