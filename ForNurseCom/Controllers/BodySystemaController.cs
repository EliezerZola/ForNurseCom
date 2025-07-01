using ForNurseCom.ModelsMaria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ForNurseCom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodySystemController : ControllerBase
    {
        private KmedicDbContext dbC = new KmedicDbContext();

        #region getALll Bodysystemas


        [HttpGet]
        public IEnumerable<BodySystema> Get()
        {
            return dbC.BodySystemas.ToList();
        }
        #endregion


  

    }
}