namespace WebApiApplication.Models
{
    public class UsersList
    {
        public IEnumerable<User> Users { get; set; } 

        public UsersList(IEnumerable<User> users)
        {
            Users = users;
        }
    }
}
