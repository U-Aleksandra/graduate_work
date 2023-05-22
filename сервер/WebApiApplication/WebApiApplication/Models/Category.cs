using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApplication.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public List<NameService>? nameServices { get; private set; }
        public List<Specialist>? Specialist { get; private set; }
    }
}
