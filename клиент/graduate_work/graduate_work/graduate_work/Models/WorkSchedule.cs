using System;
using System.Collections.Generic;
using System.Text;

namespace graduate_work.Models
{
    internal class WorkSchedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartWork { get; set; }
        public TimeSpan EndWork { get; set; }
        public TimeSpan StartBreak { get; set; }
        public TimeSpan EndBreak { get; set; }
        public Specialist Specialist { get; set; } = null;

        public WorkSchedule(DateTime date, TimeSpan startWork, TimeSpan endWork, TimeSpan startBreak, TimeSpan endBreak)
        {
            Date = date;
            StartWork = startWork;
            EndWork = endWork;
            StartBreak = startBreak;
            EndBreak = endBreak;
        }
    }
}
