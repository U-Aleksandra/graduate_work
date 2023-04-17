using System;
using System.Collections.Generic;
using System.Text;

namespace graduate_work.Models
{
    public class NameService
    {
        public int Id { get; set; }
        public string nameService { get; set; }
        public Category Category { get; private set; }
        public List<Service> Services { get; private set; }
    }
}
