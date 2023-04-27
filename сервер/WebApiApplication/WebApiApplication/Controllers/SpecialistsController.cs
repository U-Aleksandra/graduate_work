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
                    try
                    {
                        Category category = await _adp.Categories.FirstOrDefaultAsync(c => c.Name == specialist.NameCategory);
                        if(category != null)
                        {
                            await _adp.Specialists.AddAsync(specialist);
                            specialist.Category = category;
                            await _adp.SaveChangesAsync();
                            return Ok(specialist.Category);
                        }
                    }
                    catch(Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                    
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
                    outdatedSpecialist.Name = specialist.Name;
                    outdatedSpecialist.Phone = specialist.Phone;
                    outdatedSpecialist.Description = specialist.Description;
                    outdatedSpecialist.Address = specialist.Address;
                    _adp.SaveChanges();
                    return Ok("Изменения сохранены");
                }
                else return NotFound();
            }
           else return StatusCode((int)HttpStatusCode.Forbidden);
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            List<Category> listCategory =  _adp.Categories.ToList();
            if (listCategory.Any())
            {
                return Ok(listCategory);
            }
            else return NoContent();
        }
    }
}
