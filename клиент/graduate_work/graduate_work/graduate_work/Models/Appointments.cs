using System;
using System.Collections.Generic;
using System.Text;

namespace graduate_work.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public DateTime DateApointment { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Note { get; set; } = null;
        public Specialist Specialist { get; set; }
        public User User { get; set; }

        public Appointments(DateTime dateApointment, TimeSpan startTime, TimeSpan endTime, string note) 
        {
            DateApointment = dateApointment;
            StartTime = startTime;
            EndTime = endTime;
            Note = note;
        }
    }
}
