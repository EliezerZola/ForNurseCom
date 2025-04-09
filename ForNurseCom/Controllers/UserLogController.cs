using ForNurseCom.Models; using ForNurseCom.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForNurseCom.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogController : ControllerBase
    {
        //the connection context
        private KMedicContext dbC = new KMedicContext();

        #region getALll Userlogs
        // GET: api/<UserLogController>
        [HttpGet]
        public IEnumerable<Userlog> Get()
        {
            return dbC.Userlogs.ToList();
        }
        #endregion

        #region get logs by username
        // GET api/<UserLog>/username
        [HttpGet("{Username}")]
        public IEnumerable <Userlog> GetALL(string Username)
        {
            var query = dbC.Userlogs.AsQueryable(); // Use '==' for comparison instead of '='

            {
                if (string.IsNullOrEmpty(Username))
                { throw new ArgumentException("Username must be provided."); }
                else
                {
                    query = query.Where(s => s.Username == Username);

                }

                return query.ToList();
            }

        }
        #endregion



        #region Post method
        // POST api/<UserLogController>
        [HttpPost]
        public string Post([FromBody] Userlog value)
        {
            //checking if the user exist in the database

            if (!dbC.Userlogs.Any(u => u.Id.Equals(value.Id)))
            {
                Userlog user = new Userlog();

                user.Id = value.Id;
                user.Username = value.Username;
                user.Logintime = value.Logintime;

               
                    //Add to datbase
                    try
                    {
                        dbC.Userlogs.Add(user);
                        dbC.SaveChanges();
                        return JsonConvert.SerializeObject("Completed");
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(ex.Message);
                    }
            }
            else
            {
                return JsonConvert.SerializeObject("Try again");
            }
        }


        #endregion

        #region Delete
        // DELETE api/<Userlog>/username
        [HttpDelete("{Username}")]
        public string Delete(string Username)
        {
            try
            {
                // Find all user logs matching the given username
                var userLogsToDelete = dbC.Userlogs.Where(u => u.Username.Equals(Username)).ToList();

                // Check if any records exist for the given username
                if (userLogsToDelete.Count == 0)
                {
                    return $"No user logs found with Username: {Username}";
                }

                // Delete all matching records
                dbC.Userlogs.RemoveRange(userLogsToDelete);
                dbC.SaveChanges();

                return $"Successfully deleted all user logs with Username: {Username}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        // DELETE api/<UserLogController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
