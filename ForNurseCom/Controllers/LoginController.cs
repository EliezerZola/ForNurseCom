using ForNurseCom.Models; using ForNurseCom.Data;
using ForNurseCom.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForNurseCom.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //the connection context
        private KMedicContext dbC = new KMedicContext();

        #region Post method
        // POST api/<NurseController>
        [HttpPost]
        public string Post( string logname, string logpw)
        {
            //checking if the user exist in the database

            logname = "";
            logpw = "";

            var zola = Common.Hashpassord(logpw);

            if (!dbC.Users.Any(u => u.Username.Equals(logname) && u.UserPassword.Equals(zola)))
            {



                var user= dbC.Users.FirstOrDefault(u => u.Username.Equals(logname));



                //return user;
                return JsonConvert.SerializeObject(user);
            }
            else
            {
                //return JsonConvert.SerializeObject("Nurse is already in the System");
                return null;
            }
        }


        #endregion

        
    }

}


