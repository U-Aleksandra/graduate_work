using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace WebApiApplication.Models
{
    public class WorkSchedule
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan StartWork { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan EndWork { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan StartBreak { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan EndBreak { get; set; }
        public Specialist? Specialist { get; set; }

        public WorkSchedule(DateTime date,TimeSpan startWork, TimeSpan endWork, TimeSpan startBreak, TimeSpan endBreak)
        {
            Date = date;
            StartWork = startWork;
            EndWork = endWork;
            StartBreak = startBreak;
            EndBreak = endBreak;
        }
    }
}
