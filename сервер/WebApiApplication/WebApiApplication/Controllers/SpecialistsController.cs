using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApiApplication.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController : ControllerBase
    {
        private AppDatabaseContext _adp;
        public SpecialistsController(AppDatabaseContext adp)
        {
            _adp = adp;
        }
        // POST api/<SpecialistsController>
        [HttpPost("Regist")]
        public async Task<IActionResult> Regist(Specialist specialist)
        {
            if(specialist != null)
            {
                if (await _adp.Specialists.FirstOrDefaultAsync(s => s.Phone == specialist.Phone) == null)
                {
                    await _adp.Specialists.AddAsync(specialist);
                    await _adp.SaveChangesAsync();
                    return Ok(specialist);
                }
                else
                {
                    return BadRequest("Данный пользователь уже зарегистрирован в системе");
                }
            }
            return BadRequest("Данные не сохранились");
        }

        // GET api/<ValuesController>/5
        [HttpGet]
        public Array Get()
        {
            /*IEnumerable<User> users = _adp.Users.ToArray();
            UsersList usersList = new UsersList(users);*/
            Array users = _adp.Specialists.ToArray();

            return users;
        }

        // GET api/<ValuesController>/5
        /*[HttpGet("{id}")]
        public User Get(int id)
        {
            return _adp.Specialists.FirstOrDefault(s => s.Id == id).User;
            *//*int idUser = _adp.Specialists.Find(id).userId;
            return _adp.Users.Find(idUser).Name;*//*

        }*/
    }
}
