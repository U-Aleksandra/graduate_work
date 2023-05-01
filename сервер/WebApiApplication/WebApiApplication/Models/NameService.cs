namespace WebApiApplication.Models
{
    public class NameService
    {
        public int Id { get; set; }
        public string nameService { get; set; }
        public Category? Category { get;  set; }
        public List<Service>? Services { get;  set; }
    }
}
