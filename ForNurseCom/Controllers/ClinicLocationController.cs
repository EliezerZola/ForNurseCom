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
    public class ClinicLocationController : ControllerBase
    {
        //the connection context
        private KmedicDbContext dbC = new KmedicDbContext();



        #region get all
        // GET: api/<ClinicLocationController>
        [HttpGet]
        public IEnumerable<ClinicLocation> Get()
        {
            return dbC.ClinicLocations.ToList();
        }
        #endregion




        //#region POst
        //// POST api/<BodySystemaController>
        //[HttpPost]
        //public String Post([FromBody] BodySystemaController value)
        //{
        //    //checking if the user exist in the database

        //    if (!dbC.BodySystemas.Any(u => u.IdBoL.Equals(value.IdBoL)))
        //    {
        //        BodySystema user = new BodySystema();

        //        user.BodySysId = value.BodySysId;
        //        user.BodySysName = value.BodySysName ;
               



        //        //Add to datbase
        //        try
        //        {
        //            dbC.ListHospitals.Add(user);
        //            dbC.SaveChanges();
        //            return JsonConvert.SerializeObject($"{value.HosName} was added successfully");
        //        }
        //        catch (Exception ex)
        //        {
        //            return JsonConvert.SerializeObject(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return JsonConvert.SerializeObject($"{value.HosName} already exist try to update the value instead");
        //    }
        //}
        //#endregion

      



        //#region Delete
        //// DELETE api/<Dreugss>/5

        //[HttpDelete("{Id}")]
        //public string Delete(string Id)
        //{
        //    try
        //    {
        //        Drug drug = dbC.Drugs.Find(Id);
        //        if (drug != null)
        //        {
        //            dbC.Drugs.Remove(drug);
        //            dbC.SaveChanges();
        //            return "drug Data Deleted";
        //        }
        //        else
        //        {
        //            return $"drug Data Not found with  ID:" + (Id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //#endregion
    }
}
