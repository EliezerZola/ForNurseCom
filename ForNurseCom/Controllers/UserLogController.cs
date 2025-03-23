using ForNurseCom.ModelsMaria;
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
        private KmedicDbContext dbC = new KmedicDbContext();

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
        // DELETE api/<Userlog>/5  this line delete the user logs based on a username
        [HttpDelete("{Username}")]
        public string Delete(string Username)
        {
            try
            {
                Userlog drug = dbC.Userlogs.Find(Username);
                if (drug != null)
                {
                    dbC.Userlogs.Remove(drug);
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
