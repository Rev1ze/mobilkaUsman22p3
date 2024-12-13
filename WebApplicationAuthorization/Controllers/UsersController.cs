using Microsoft.AspNetCore.Mvc;
using rmpAuthorization;
using WebApplicationAuthorization.models;
using System.Diagnostics.Metrics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("UsersAll")]
        public ActionResult<User> GetAllUsers()
        {
            List<User> users = Getter.getUsers();
            if (users is null || users.Count == 0)
            {   
                return NotFound("Пользователи не найдены");
            }
            return Ok(JsonConvert.SerializeObject(users));
        }


        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user_id = Getter.getUsers().FirstOrDefault(user => user.Id == id);
            return user_id == null ? NotFound("Пользователь не найден") : Ok(user_id);
        }
        [HttpPost("Authorization")]
        public ActionResult<User> authorization(string login, string passw)
        {
            var check_user = Getter.getUsers().FirstOrDefault(u => u.Email == login && u.Password == passw);
            return check_user == null ? NotFound("Пользователь не найден") : Ok(check_user);
        }
        [HttpPost("Authorization2")]
        public ActionResult<User> authorization2([FromBody] LogRequest user)
        {
            var check_user = Getter.getUsers().FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            return check_user == null ? NotFound("Пользователь не найден") : Ok(check_user);
        }

        [HttpPost("Registration")]
        public ActionResult<User> Register(User user)
        {
            string json = System.IO.File.ReadAllText(Getter.path);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
            user.Id = maxId + 1;
            if (users.Exists(x=> x.Email == user.Email))
            {
                return BadRequest("Ваш электронный адрес уже существует");
            }
            if (Convert.ToDateTime(user.Birthday) > Convert.ToDateTime($"{DateTime.Now.Year - 18}.{DateTime.Now.Month}.{DateTime.Now.Day}"))
            {
                return BadRequest("Вам слишком мало лет");
            }
            user.IsBlocked = false;
            users.Add(user);
            string newJson = JsonConvert.SerializeObject(users);
            System.IO.File.WriteAllText(Getter.path , newJson);
            return Ok("Пользователь зарегистрирован");
        }
        [HttpPost("Registration2")]
        public ActionResult<User> Register2(User user)
        {
            string json = System.IO.File.ReadAllText(Getter.path);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
            user.Id = maxId + 1;
            if (users.Exists(x=> x.Email == user.Email))
            {
                return BadRequest("Ваш электронный адрес уже существует");
            }
            if (user.Password.Length < 6)
            {
                return BadRequest("Слишком короткий пароль");
            }
            if (user.isValidEmail())
            {
                return BadRequest("Неправильная почта");
            }
            if (Convert.ToDateTime(user.Birthday) > Convert.ToDateTime($"{DateTime.Now.Year - 18}.{DateTime.Now.Month}.{DateTime.Now.Day}"))
            {
                return BadRequest("Вам слишком мало лет");
            }
            user.IsBlocked = false;
            users.Add(user);
            string newJson = JsonConvert.SerializeObject(users);
            System.IO.File.WriteAllText(Getter.path , newJson);
            return Ok("Пользователь зарегистрирован");
        }

        [HttpPut("EditUser")]
        public ActionResult<User> EditUser(int iduser, User user)
        {
            var users = Getter.getUsers() ?? new List<User>();
            var user_check = users.FirstOrDefault(u => u.Id == iduser);
            if (user.Phone.Length != 11 || !user.Phone.All(char.IsDigit) || !user.Phone.StartsWith('8'))
            {
                return BadRequest("Неверный номер телефона");
            }
            if (user.isValidEmail())
            {
                return BadRequest("Неверная почта");
            }
            if (user_check != null)
            {
                user.IsBlocked = false; user.Email = user_check.Email;
                user.Id = user_check.Id;
                users.Remove(user_check);
                users.Add(user);
                string newjson = JsonConvert.SerializeObject(users);
                System.IO.File.WriteAllText(Getter.path, newjson);
                return CreatedAtAction(nameof(Register), "Вы успешно поменяли данные");
            }
            else
            {
                return BadRequest("Пользователь не существует");
            }
        }
        [HttpPut("DeleteUser")]
        public ActionResult<User> DeleteUser(int id)
        {
            string json = System.IO.File.ReadAllText(Getter.path);
            var users = Getter.getUsers() ?? new List<User>();
            var user_check = users.FirstOrDefault(u => u.Id == id);
            if (user_check != null)
            {
                users.Remove(user_check);
                string newJson = JsonConvert.SerializeObject(users);
                System.IO.File.WriteAllText(Getter.path, newJson);
                return CreatedAtAction(nameof(Register), "Ваш профиль удален");

            }
            else
            {
                return BadRequest("Пользователь не найден");
            }
        }

    }
}
