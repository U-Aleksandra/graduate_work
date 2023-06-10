using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApplication.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string? DescriptionService { get; set; }

        [Column(TypeName = "decimal(20, 0)")]
        public decimal Price { get; set; }
        public bool StartPrice { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan ServicesTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan BreakTime { get; set; }
        public NameService? NameService { get; set; }
        public Specialist? Specialist { get;  set; }

        public Service(string descriptionService, decimal price, bool startPrice, TimeSpan servicesTime, TimeSpan breakTime)
        {
            DescriptionService = descriptionService;
            Price = price;
            StartPrice = startPrice;
            ServicesTime = servicesTime;
            BreakTime = breakTime;
        }
        public Service(int id, string descriptionService, decimal price, bool startPrice, TimeSpan servicesTime, TimeSpan breakTime)
        {
            Id = id;
            DescriptionService = descriptionService;
            Price = price;
            StartPrice = startPrice;
            ServicesTime = servicesTime;
            BreakTime = breakTime;
        }
    }
}
