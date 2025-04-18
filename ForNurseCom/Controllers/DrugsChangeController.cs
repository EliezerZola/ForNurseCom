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
    public class DrugsChangeController : ControllerBase
    {
        //the connection context
        private KmedicDbContext dbC = new KmedicDbContext();

        #region getALll Drugchange
        // GET: api/DrugchangeLogController>
        //[HttpGet]
        //public IEnumerable<DrugChange> Get()
        //{
        //    return dbC.DrugChanges.ToList();
        //}
        #endregion

        #region getALll Drugchange
        // GET: api/DrugchangeLogController
        [HttpGet]
        public IEnumerable<Drugchange> Get()
        {
            return dbC.Drugchanges
                      .OrderBy(dc => dc.MedLocation)
                      .ThenBy(dc => dc.MedName)
                      .ThenBy(dc => dc.TimePrescribe)
                      .ToList();
        }
        #endregion


        #region Post method
        // POST api/<DrugChangeController>
        [HttpPost]
        public string Post([FromBody] Drugchange value)
        {
            //checking if the user exist in the database

            if (!dbC.Drugchanges.Any(u => u.Id.Equals(value.Id)))
            {
                Drugchange user = new Drugchange();

                user.Id = value.Id;
                user.MedName = value.MedName;
                user.MedQuantity = value.MedQuantity;
                user.MedLocation = value.MedLocation;
                user.TimePrescribe = value.TimePrescribe;


                //Add to datbase
                try
                {
                    dbC.Drugchanges.Add(user);
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
                return JsonConvert.SerializeObject("Change not implemented");
            }
        }


        #endregion

        #region get Med change details grouped by day
        // GET api/<UserLog>/location
        [HttpGet("{MedLoc}")]
        public IEnumerable<dynamic> GetPrescribedDrugsGrouped(string MedLoc)
        {
            if (string.IsNullOrEmpty(MedLoc))
            {
                throw new ArgumentException("Medicine location must be provided.");
            }

            var past30Days = DateTime.Now.AddDays(-30);

            var query = dbC.Drugchanges
                .Where(s => s.MedLocation == MedLoc && s.TimePrescribe >= past30Days)
                .GroupBy(s => new
                {
                    MedName = s.MedName,
                    MedLocation = s.MedLocation,
                    PrescribedDay = s.TimePrescribe // Still grouping by date (not formatted)
                })
                .Select(group => new
                {
                    MedName = group.Key.MedName,
                    MedLocation = group.Key.MedLocation,
                    PrescribedDay = group.Key.PrescribedDay, // Return as DateTime
                    TotalQuantity = group.Sum(s => s.MedQuantity)
                })
                .OrderBy(result => result.PrescribedDay) // Sort by day
                .ToList();

            return query;
        }
        #endregion

        #region Delete
        // DELETE api/<DrugChange>/Name  this line delete the user logs based on a username
        [HttpDelete("{Medname}")]
        public string Delete(string Medname)
        {
            try
            {
                Drugchange drug = dbC.Drugchanges.Find(Medname);
                if (drug != null)
                {
                    dbC.Drugchanges.Remove(drug);
                    dbC.SaveChanges();
                    return "drug Data Deleted";
                }
                else
                {
                    return $"drug Data Not found with  ID:" + (Medname);
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
