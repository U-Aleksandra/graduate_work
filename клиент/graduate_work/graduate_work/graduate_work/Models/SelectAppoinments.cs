using System;
using System.Collections.Generic;
using System.Text;

namespace graduate_work.Models
{
    public class SelectAppoinments
    {
        public int Id { get; set; }
        public DateTime DateApointment { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Note { get; set; } = null;
        public string NameSpecialist { get; set; }
        public string NameUser { get; set; }
        public string Address { get; set; }
    }
}
