using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApplication.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string DescriptionService { get; set; }

        [Column(TypeName = "decimal(20, 0)")]
        public decimal Price { get; set; }
        public bool StartPrice { get; set; }
        public DateTime ServicesTime { get; set; }
        public DateTime BreakTime { get; set; }
        public NameService? NameService { get; set; }
        public Specialist? Specialist { get;  set; }

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
