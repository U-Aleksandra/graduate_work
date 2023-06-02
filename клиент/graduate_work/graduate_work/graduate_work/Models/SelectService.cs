using System;
using System.Collections.Generic;
using System.Text;

namespace graduate_work.Models
{
    public class SelectService
    {
        public int Id { get; set; }
        public Specialist specialist { get; set; }
        public string nameService { get; set; }
        public decimal Price { get; set; }
        public bool StartPrice { get; set; }
        public TimeSpan ServicesTime { get; set; }
        public TimeSpan BreakTime { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DescriptionService { get; set; }
    }
}
