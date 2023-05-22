using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApplication.Models
{
    public class NameService
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string nameService { get; set; }
        public Category? Category { get;  set; }
        public List<Service>? Services { get;  set; }
    }
}
