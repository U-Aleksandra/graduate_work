using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Models;
using System.Net;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private AppDatabaseContext _adp;
        public UsersController(AppDatabaseContext adp)
        {
            _adp = adp;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public Array Get()
        {
            /*IEnumerable<User> users = _adp.Users.ToArray();
            UsersList usersList = new UsersList(users);*/
            Array users = _adp.Users.ToArray();

            return users;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            return _adp.Users.Find(id).Name;
        }

        // POST api/<ValuesController>
        [HttpPost("Regist")]
        public async Task<IActionResult> PostRegist(User user)
        {
            if (user != null)
            {
                if (await _adp.Users.FirstOrDefaultAsync(u => u.Phone == user.Phone) == null)
                {
                    await _adp.Users.AddAsync(user);
                    await _adp.SaveChangesAsync();
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Данный пользователь уже зарегистрирован в системе");
                }
            }
            return BadRequest("Данные не сохранились");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> PostLogin(User user)
        {
            if (user != null)
            {
                User? searchUser = await _adp.Users.FirstOrDefaultAsync(u => u.Phone == user.Phone && u.Password == user.Password);
                if (searchUser != null)
                {
                    return Ok(searchUser);
                }
                return BadRequest("Данный пользователь не зарегистрирован в системе");
            }
            return BadRequest();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string name)
        {
            User user = _adp.Users.Find(id);
            user.Name = name;
            _adp.SaveChanges();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User user = _adp.Users.Find(id);
            _adp.Users.Remove(user);
            _adp.SaveChanges();
        }
    }
}
