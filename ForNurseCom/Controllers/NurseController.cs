using ForNurseCom.Models; using ForNurseCom.Data;
using ForNurseCom.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForNurseCom.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NurseController : ControllerBase
    {
        //the connection context
        private KMedicContext dbC = new KMedicContext();

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

            if (!dbC.Users.Any(u => u.Userid.Equals(value.Userid)))
            {
                User user = new User();

                user.Userid = value.Userid;
                user.Username = value.Username;
                user.UserPassword = Common.Hashpassord(value.UserPassword);
                user.UserSalt = Common.Hashpassord(value.UserSalt);

                if (value.UserPassword != value.UserSalt)
                {
                    return JsonConvert.SerializeObject("The two passwords don't match");
                }
                else if (dbC.Users.Any(u => u.Username.Equals(value.Username)))
                {
                    return JsonConvert.SerializeObject($"There is an existing user with name {value.Username}");
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
        [HttpDelete("{Userid}")]
            public string Delete(string Userid)
            {
                try
                {
                    User nurse = dbC.Users.Find(Userid);
                    if (nurse != null)
                    {
                        dbC.Users.Remove(nurse);
                        dbC.SaveChanges();
                        return "Staff Data Deleted";
                    }
                    else
                    {
                        return $"Nurse Data Not found with  ID:" + (Userid);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        // PUT api/<NurseController>
        [HttpPut]
        public async Task<IActionResult> Put(string Id, User value)
        {
            try
            {
                // Validate input
                if (value == null || string.IsNullOrEmpty(Id))
                    return BadRequest("Invalid request data");

                // Find existing user
                var existingUser = await dbC.Users.FindAsync(Id);
                if (existingUser == null)
                    return NotFound($"User with ID {Id} not found");

                // Password validation
                if (value.UserPassword != value.UserSalt)
                    return BadRequest("Password and confirmation don't match");

                // Username uniqueness check (excluding current user)
                if (dbC.Users.Any(u => u.Username == value.Username && u.Userid != Id))
                    return Conflict($"Username {value.Username} is already taken");

                // Update properties
                existingUser.Username = value.Username;

                // Only update password if it's being changed
                if (!string.IsNullOrEmpty(value.UserPassword))
                {
                    existingUser.UserPassword = Common.Hashpassord(value.UserPassword);
                    existingUser.UserSalt = Common.Hashpassord(value.UserSalt);
                }

                // Save changes
                dbC.Entry(existingUser).State = EntityState.Modified;
                await dbC.SaveChangesAsync();

                return Ok(new
                {
                    Message = $"{value.Username} updated successfully",
                    User = existingUser
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        #endregion


    }

}


