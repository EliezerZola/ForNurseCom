using ForNurseCom.Models;
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
        private MyDbContext dbC = new MyDbContext();

        #region getALll Userlogs
        // GET: api/<UserLogController>
        [HttpGet]
        public IEnumerable<UserLog> Get()
        {
            return dbC.UserLogs.ToList();
        }
        #endregion

        #region get logs by username
        // GET api/<UserLog>/username
        [HttpGet("{Username}")]
        public IEnumerable <UserLog> GetALL(string Username)
        {
            var query = dbC.UserLogs.AsQueryable(); // Use '==' for comparison instead of '='

            {
                if (string.IsNullOrEmpty(Username))
                { throw new ArgumentException("Username must be provided."); }
                else
                {
                    query = query.Where(s => s.UserName == Username);

                }

                return query.ToList();
            }

        }
        #endregion



        #region Post method
        // POST api/<UserLogController>
        [HttpPost]
        public string Post([FromBody] UserLog value)
        {
            //checking if the user exist in the database

            if (!dbC.UserLogs.Any(u => u.Id.Equals(value.Id)))
            {
                UserLog user = new UserLog();

                user.Id = value.Id;
                user.UserName = value.UserName;
                user.Logintime = value.Logintime;

               
                    //Add to datbase
                    try
                    {
                        dbC.UserLogs.Add(user);
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
        // DELETE api/<Userlog>/5  this line delete the user logs based on a username
        [HttpDelete("{Username}")]
        public string Delete(string Username)
        {
            try
            {
                UserLog drug = dbC.UserLogs.Find(Username);
                if (drug != null)
                {
                    dbC.UserLogs.Remove(drug);
                    dbC.SaveChanges();
                    return "drug Data Deleted";
                }
                else
                {
                    return $"drug Data Not found with  ID:" + (Username);
                }
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
