using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Models;
using System.Net;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.VisualBasic;
using System.Runtime.Intrinsics.Arm;

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

        [HttpGet("GetWorkDays")]
        public async Task<IActionResult> GetWorkDays(int idSpecialist)
        {
            List<WorkSchedule> listWorkSchedule = _adp.WorkSchedules.Where(w => w.Specialist.Id == idSpecialist && w.Date >= DateTime.Today).ToList();
            if (listWorkSchedule.Any())
            {
                return Ok(listWorkSchedule);
            }
            else return NoContent();
        }

        [HttpGet("GetOfFreeTime")]
        public async Task<IActionResult> GetOfFreeTime(DateTime dateWork, int idSpecialist, TimeSpan serviceTime, TimeSpan serviceBreak)
        {
            TimeSpan intervalTime = new(0, 0, 5, 0, 0);
            TimeSpan fullTime = serviceTime + serviceBreak;
            List<TimeSpan> listTimeBreak = new();
            List<Tuple<TimeSpan, bool>> listTimeWork = new();
            List<TimeSpan> listServiceTime = new();
            List<TimeSpan> listFreeTime = new();

            WorkSchedule? workSchedule = await _adp.WorkSchedules.FirstOrDefaultAsync(w => w.Date == dateWork && w.Specialist.Id == idSpecialist);
            //разбиение рабочего графика на промежутки
            if(workSchedule.StartWork.ToString() != "00:00:00" && workSchedule.EndWork.ToString() != "00:00:00")
            {
                listTimeWork.Add(new Tuple<TimeSpan, bool>(workSchedule.StartWork, true));
                while (workSchedule.StartWork != workSchedule.EndWork)
                {
                    workSchedule.StartWork += intervalTime;
                    listTimeWork.Add(new Tuple<TimeSpan, bool>(workSchedule.StartWork, true));
                }
                listTimeWork.RemoveAt(listTimeWork.Count - 1);
            }
            //разбиение обеденного перерыва на промежутки
            if (workSchedule.StartBreak.ToString() != "00:00:00" && workSchedule.EndBreak.ToString() != "00:00:00")
            {
                listTimeBreak.Add(workSchedule.StartBreak);
                while (workSchedule.StartBreak != workSchedule.EndBreak)
                {
                    workSchedule.StartBreak += intervalTime;
                    listTimeBreak.Add(workSchedule.StartBreak);
                }
                listTimeBreak.RemoveAt(listTimeBreak.Count - 1);
            }
            //разбиение занятых окошек на промежутки
            List<Appointments> listAppointments = _adp.Appointments.Where(a => a.DateApointment == dateWork && a.Specialist.Id == idSpecialist).ToList();
            List<TimeSpan> listBusyTime = new();
            foreach (var item in listAppointments)
            {
                listBusyTime.Add(item.StartTime);
                while (item.StartTime != item.EndTime)
                {
                    item.StartTime += intervalTime;
                    listBusyTime.Add(item.StartTime);
                }
                listBusyTime.RemoveAt(listBusyTime.Count - 1);
            }
            //разбиение продолжительность услуги на промежутки
            TimeSpan startTime = new(0, 0, 0);
            if(fullTime.ToString() != "00:00:00")
            {
                listServiceTime.Add(startTime);
                while (startTime != fullTime)
                {
                    startTime += intervalTime;
                    listServiceTime.Add(startTime);
                }
                listServiceTime.RemoveAt(listServiceTime.Count - 1);
            }

            for (var i = 0; i < listTimeWork.Count; i++)
            {
                if (listTimeBreak.Contains(listTimeWork[i].Item1) || listBusyTime.Contains(listTimeWork[i].Item1))
                {
                    listTimeWork[i] = new Tuple<TimeSpan, bool>(listTimeWork[i].Item1, false);
                }
            };

            for (var i = 0; i < listTimeWork.Count; i++)
            {
                int count = 0;
                for (var j = i; j < listTimeWork.Count; j++)
                {
                    if (listTimeWork[j].Item2) count++;
                    else break;

                    if (count == listServiceTime.Count)
                    {
                        listFreeTime.Add(listTimeWork[i].Item1);
                        break;
                    }
                }
            }
            return Ok(listFreeTime);  
        }

        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment(Appointments appointments)
        {
            Specialist? specialist = await _adp.Specialists.FirstOrDefaultAsync(s => s.Id == appointments.Specialist.Id);
            User? user = await _adp.Users.FirstOrDefaultAsync(u => u.Id == appointments.User.Id);
            if(specialist.Id == user.Id)
            {
                return BadRequest("Специалист не может записаться к себе");
            }
            if(specialist != null && user != null)
            {
                /*appointments.Specialist = null;
                appointments.User = null;
                _adp.Appointments.Add(appointments);
                appointments.Specialist = specialist;
                appointments.User = user;
                _adp.SaveChanges();
                return Ok("Запись создана");*/
                //var item = _adp.Appointments.FirstOrDefaultAsync(a => a.DateApointment == appointments.DateApointment &&
                    //a.StartTime == appointments.StartTime && a.User.Id == appointments.User.Id);
                if (await _adp.Appointments.FirstOrDefaultAsync(a => a.DateApointment == appointments.DateApointment &&
                    a.StartTime == appointments.StartTime && a.User.Id == appointments.User.Id) == null)
                {
                    appointments.Specialist = null;
                    appointments.User = null;
                    _adp.Appointments.Add(appointments);
                    appointments.Specialist = specialist;
                    appointments.User = user;
                    _adp.SaveChanges();
                    return Ok("Запись создана");
                }
                else return BadRequest("У пользователя уже есть запись на это время");
            }
            return BadRequest();
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
