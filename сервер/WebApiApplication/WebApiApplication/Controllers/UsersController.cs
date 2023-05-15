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
                    if (searchUser.isSpecialist)
                    {
                        if (searchUser is Specialist specialist)
                        {
                            Category? category = await _adp.Categories.FirstOrDefaultAsync(c => c.Name == specialist.NameCategory);
                            specialist.Category = category;
                            return Ok(specialist);
                        }
                    }
                    return Ok(searchUser);
                }
                return BadRequest("Данный пользователь не зарегистрирован в системе");
            }
            return BadRequest();
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            User? outdatedUser = await _adp.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();
            if (outdatedUser != null)
            {
                outdatedUser.Name = user.Name;
                outdatedUser.Phone = user.Phone;
                _adp.SaveChanges();
                return Ok("Изменения сохранены");
            }
           else return NotFound();
        }

        [HttpGet("GetServicesByCategory")]
        public async Task<IActionResult> GetServicesByCategory(int categoryId)
        {
            List<NameService> listOfServices = _adp.NameServices.Where(ns => ns.Category.Id == categoryId).ToList();
            if (listOfServices.Any())
            {
                return Ok(listOfServices);
            }
            else return NoContent();
        }

        [HttpGet("GetServicesByName")]
        public async Task<IActionResult> GetServicesByName(int nameServisesId)
        {
            var nameServices = await _adp.NameServices.Include(s => s.Services).FirstOrDefaultAsync(ns => ns.Id == nameServisesId);
            if (nameServices.Services != null)
            {
                foreach (var item in nameServices.Services)
                {
                    item.Specialist = await _adp.Specialists.FirstOrDefaultAsync(s => s.Services.Contains(item));
                }
                return Ok(nameServices.Services);
            }
            else return NoContent();
        }

        [HttpGet("GetServicesBySpecialist")]
        public async Task<IActionResult> GetServicesBySpecialist(int idSpecialist)
        {
            List<Service> listService = _adp.Services.Include(s => s.NameService).Where(s => s.Specialist.Id == idSpecialist).ToList();
            if (listService.Any())
            {
                return Ok(listService);
            }
            else return NoContent();
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
