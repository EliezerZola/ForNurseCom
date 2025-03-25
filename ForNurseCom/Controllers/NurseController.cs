using ForNurseCom.ModelsMaria;
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
        private KmedicDbContext dbC = new KmedicDbContext();

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

        #region Put method
        // PUT api/<NurseController>
        [HttpPut]
        public string Put([FromBody] User value)
        {
            try
            {
                // Check if the user exists in the database
                var user = dbC.Users.FirstOrDefault(u => u.Userid.Equals(value.Userid));

                if (user == null)
                {
                    // If the user does not exist, return a message
                    return JsonConvert.SerializeObject("User not found. Cannot update.");
                }

                // Update user information
                user.Username = value.Username ?? user.Username; // Keep existing value if new value is null
                user.UserPassword = value.UserPassword != null ? Common.Hashpassord(value.UserPassword) : user.UserPassword;
                user.UserSalt = value.UserSalt != null ? Common.Hashpassord(value.UserSalt) : user.UserSalt;

                // Ensure passwords match before saving
                if (value.UserPassword != null && value.UserPassword != value.UserSalt)
                {
                    return JsonConvert.SerializeObject("The two passwords don't match");
                }

                // Save changes to the database
                dbC.SaveChanges();

                return JsonConvert.SerializeObject("User information updated successfully");
            }
            catch (Exception ex)
            {
                // Log and return the exception message
                Console.WriteLine(ex.ToString()); // Log full exception for debugging
                return JsonConvert.SerializeObject(ex.InnerException?.Message ?? ex.Message);
            }
        }
        #endregion


    }

}


