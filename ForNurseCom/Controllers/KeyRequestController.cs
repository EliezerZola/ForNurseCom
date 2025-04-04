﻿using ForNurseCom.ModelsMaria;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForNurseCom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyRequestController : ControllerBase
    {
        KmedicDbContext dbC = new KmedicDbContext();

        #region get Med chnage based on Name and location
        // GET api/<KeyrEQUEST>/username
        [HttpGet("{username}")]
        public IEnumerable<Keyrequest> GetALL(string username)
        {
            var query = dbC.Keyrequests.AsQueryable(); // Use '==' for comparison instead of '='

            {
                if (string.IsNullOrEmpty(username))
                {
                    username = "Nurses";
                    query = query.Where(s => s.Username == username);
                }
                else
                {
                    username = "Nurses";
                    query = query.Where(s => s.Username == username);

                }

                return query;
            }

        }
        #endregion

    }
}
