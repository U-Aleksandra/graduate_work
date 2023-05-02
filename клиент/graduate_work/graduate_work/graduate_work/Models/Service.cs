using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace graduate_work.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string DescriptionService { get; set; }
        public decimal Price { get; set; }
        public bool StartPrice { get; set; }
        public DateTime ServicesTime { get; set; }
        public DateTime BreakTime { get; set; }
        public NameService NameService { get; set; } = null;
        public Specialist Specialist { get; set; } = null;

        public Service(string descriptionService, decimal price, bool startPrice, DateTime servicesTime, DateTime breakTime)
        {
            DescriptionService = descriptionService;
            Price = price;
            StartPrice = startPrice;
            ServicesTime = servicesTime;
            BreakTime = breakTime;
        }
    }
}
