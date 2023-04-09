using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApiApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        [HttpPut]
        public async Task<IActionResult> Put(Specialist specialist)
        {
            if (specialist.isSpecialist)
            {
                Specialist outdatedSpecialist = await _adp.Specialists.Where(s => s.Id == specialist.Id).FirstOrDefaultAsync();
                if (outdatedSpecialist != null)
                {
                    if (Equals(outdatedSpecialist, specialist))
                    {
                        return BadRequest("Данные не были изменены");
                    }
                    else
                    {
                        outdatedSpecialist.Name = specialist.Name;
                        outdatedSpecialist.Phone = specialist.Phone;
                        outdatedSpecialist.Description = specialist.Description;
                        outdatedSpecialist.Address = specialist.Address;
                        _adp.SaveChanges();
                        return Ok("Изменения сохранены");
                    }
                }
                else return NotFound();
            }
           else return StatusCode((int)HttpStatusCode.Forbidden);
        }
    }
}
