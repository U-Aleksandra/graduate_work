using System;
using System.Collections.Generic;
using System.Text;

namespace graduate_work.Models
{
    public class Service
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool StartPrice { get; set; }
        public DateTime ServicesTime { get; set; }
        public DateTime Break { get; set; }
        public NameService NameService { get; private set; }
        public Specialist Specialist { get; private set; }
    }
}
