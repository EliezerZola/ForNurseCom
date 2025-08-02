using ForNurseCom.ModelsMaria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForNurseCom.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        //the connection context
        private KmedicDbContext dbC = new KmedicDbContext();



        #region get all
        // GET: api/<VisitController>
        [HttpGet]
        public IEnumerable<ListHospital> Get()
        {
            return dbC.ListHospitals.ToList();
        }
        #endregion




        #region POst
        // POST api/<Dreugss>
        [HttpPost]
        public String Post([FromBody] ListHospital value)
        {
            //checking if the user exist in the database

            if (!dbC.ListHospitals.Any(u => u.HosId.Equals(value.HosId)))
            {
                ListHospital user = new ListHospital();

                user.HosId = value.HosId;
                user.HosName = value.HosName;
                user.HosAddress = value.HosAddress;
                user.HosNumber = value.HosNumber;



                //Add to datbase
                try
                {
                    dbC.ListHospitals.Add(user);
                    dbC.SaveChanges();
                    return JsonConvert.SerializeObject($"{value.HosName} was added successfully");
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }
            }
            else
            {
                return JsonConvert.SerializeObject($"{value.HosName} already exist try to update the value instead");
            }
        }
        #endregion

      



        #region Delete
        // DELETE api/<Dreugss>/5

        [HttpDelete("{Id}")]
        public string Delete(string Id)
        {
            try
            {
                Drug drug = dbC.Drugs.Find(Id);
                if (drug != null)
                {
                    dbC.Drugs.Remove(drug);
                    dbC.SaveChanges();
                    return "drug Data Deleted";
                }
                else
                {
                    return $"drug Data Not found with  ID:" + (Id);
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
