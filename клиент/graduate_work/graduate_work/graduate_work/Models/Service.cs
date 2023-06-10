using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace graduate_work.Models
{
    [Serializable]
    public class Service
    {
        public int Id { get; set; }
        public string DescriptionService { get; set; } = null;
        public decimal Price { get; set; }
        public bool StartPrice { get; set; }
        public TimeSpan ServicesTime { get; set; }
        public TimeSpan BreakTime { get; set; }
        public NameService NameService { get; set; } = null;
        public Specialist Specialist { get; set; } = null;

        [System.Text.Json.Serialization.JsonConstructor]
        public Service(string descriptionService, decimal price, bool startPrice, TimeSpan servicesTime, TimeSpan breakTime)
        {
            DescriptionService = descriptionService;
            Price = price;
            StartPrice = startPrice;
            ServicesTime = servicesTime;
            BreakTime = breakTime;
        }
        /*public Service(int id, string descriptionService, decimal price, bool startPrice, TimeSpan servicesTime, TimeSpan breakTime)
        {
            Id = id;
            DescriptionService = descriptionService;
            Price = price;
            StartPrice = startPrice;
            ServicesTime = servicesTime;
            BreakTime = breakTime;
        }*/
    }
}
