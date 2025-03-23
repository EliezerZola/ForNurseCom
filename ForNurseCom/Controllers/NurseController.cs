using ForNurseCom.Models;
using ForNurseCom.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForNurseCom.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class NurseController : ControllerBase
    {
        //the connection context
        private MyDbContext dbC = new MyDbContext();

        #region getALll Nurses
        // GET: api/<NurseController>
        
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return dbC.Users.ToList();
        }
        #endregion

        #region gett by username and passoword
        // GET api/<Nurse>/5
        [HttpGet("{EmUsername}/{EmSalt}")]
        public User Get(string EmUsername, string EmSalt)
        {
            {
                if (string.IsNullOrEmpty(EmUsername) || string.IsNullOrEmpty(EmSalt))
                { throw new ArgumentException("Username and Salt must be provided."); }

                string Zola= Common.Hashpassord(EmSalt);
                var query = dbC.Users.AsQueryable(); // Use '==' for comparison instead of '='
                return query.FirstOrDefault(s => s.Username == EmUsername && s.UserSalt == Zola);
            }

        }
        #endregion

        #region Post method
        // POST api/<NurseController>
        [HttpPost]
        public string Post([FromBody] User value)
        {
            //checking if the user exist in the database

            if (!dbC.Users.Any(u => u.UserId.Equals(value.UserId)))
            {
                User user = new User();

                user.UserId = value.UserId;
                user.Username = value.Username;
                user.UserPassword = Common.Hashpassord(value.UserPassword);
                user.UserSalt = Common.Hashpassord(value.UserSalt);

                if (value.UserPassword != value.UserSalt)
                {
                    return JsonConvert.SerializeObject("The two passwords don't match");
                }
                else
                    //Add to datbase
                    try
                    {
                        dbC.Users.Add(user);
                        dbC.SaveChanges();
                        return JsonConvert.SerializeObject("Nurse Registered");
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(ex.Message);
                    }
            }
            else
            {
                return JsonConvert.SerializeObject("Nurse is already in the System");
            }
        }


        #endregion

        #region LoginRequest
        [AllowAnonymous]
        [HttpPost("{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            var user = dbC.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username" });
            }

            if (user.UserPassword != Common.Hashpassord(password))
            {
                return Unauthorized(new { Message = "Invalid password" });
            }



            // Return the token
            return Ok(new { Message = "Login successful!" });
        } 
        #endregion



        #region Delete
        // DELETE api/<NurseController>/5
        [HttpDelete("{Emp_ID}")]
            public string Delete(string Emp_ID)
            {
                try
                {
                    User nurse = dbC.Users.Find(Emp_ID);
                    if (nurse != null)
                    {
                        dbC.Users.Remove(nurse);
                        dbC.SaveChanges();
                        return "Staff Data Deleted";
                    }
                    else
                    {
                        return $"Nurse Data Not found with  ID:" + (Emp_ID);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            #endregion

        
    }

}


