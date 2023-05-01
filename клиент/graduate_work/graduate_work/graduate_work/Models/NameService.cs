using System;
using System.Collections.Generic;
using System.Text;

namespace graduate_work.Models
{
    [Serializable]
    public class NameService
    {
        public int Id { get; set; }
        public string nameService { get; set; }
        public Category Category { get; set; }
        public List<Service> Services { get; set; }

        public NameService(string nameService) 
        { 
            this.nameService = nameService;
            Services = new List<Service>();
            Category = new Category();
        }
    }
}
