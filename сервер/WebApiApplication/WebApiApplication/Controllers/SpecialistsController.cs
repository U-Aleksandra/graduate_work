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
                            return Ok(specialist);
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
        public async Task<IActionResult> GetSpecialist()
        {
            List<Specialist> listSpecialist = _adp.Specialists.ToList();
            if (listSpecialist.Any())
            {
                return Ok(listSpecialist);
            }
            else return NoContent();
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

        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService(Service service) 
        {
            if(service != null)
            {
                if(await _adp.Services.FirstOrDefaultAsync(s => s.Specialist.Id == service.Specialist.Id &&
                s.NameService.nameService == service.NameService.nameService) == null)
                {
                    NameService? nameService = await _adp.NameServices.FirstOrDefaultAsync(ns => ns.nameService == service.NameService.nameService);
                    Specialist? specialist = await _adp.Specialists.FirstOrDefaultAsync(s => s.Id == service.Specialist.Id);
                    if(nameService != null && specialist != null)
                    {
                        service.Specialist = null;
                        service.NameService = null;
                        await _adp.Services.AddAsync(service);
                        service.NameService = nameService;
                        service.Specialist = specialist;
                        _adp.SaveChanges();
                        return Ok("Услуга создана");
                    }
                    return BadRequest("Отсутсвие необходимых данных");
                }
                else
                {
                    return BadRequest("Данная услуга уже существует");
                }
            }
            return BadRequest("Данные не сохранились");
        }

        [HttpPost("CreateWorkSchedule")]
        public async Task<IActionResult> CreateWorkSchedule(WorkSchedule workSchedule)
        {
            if(workSchedule != null)
            {
                if (await _adp.WorkSchedules.FirstOrDefaultAsync(w => w.Date == workSchedule.Date && w.Specialist.Id == workSchedule.Specialist.Id) == null)
                {
                    Specialist? specialist = await _adp.Specialists.FirstOrDefaultAsync(s => s.Id == workSchedule.Specialist.Id);
                    workSchedule.Specialist = null;
                    await _adp.WorkSchedules.AddAsync(workSchedule);
                    workSchedule.Specialist = specialist;
                    _adp.SaveChanges();
                    return Ok("Рабочий день создан");
                }
                return BadRequest("На выбранную дату уже создан рабочий день");
            }
            return BadRequest("Данные не сохранились");
        }

        [HttpGet("GetWorkShedule")]
        public async Task<IActionResult> GetWorkShedule(int specialistId)
        {
            List<WorkSchedule> listWorkSchedule = _adp.WorkSchedules.Where(w => w.Specialist.Id == specialistId).ToList();
            if (listWorkSchedule.Any())
            {
                return Ok(listWorkSchedule);
            }
            else return NoContent();
        }
    }
}
