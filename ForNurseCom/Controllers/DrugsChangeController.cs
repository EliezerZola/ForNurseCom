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
    public class DrugsChangeController : ControllerBase
    {
        //the connection context
        private MyDbContext dbC = new MyDbContext();

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
        public IEnumerable<DrugChange> Get()
        {
            return dbC.DrugChanges
                      .OrderBy(dc => dc.MedLocation)
                      .ThenBy(dc => dc.MedName)
                      .ThenBy(dc => dc.TimePrescribed)
                      .ToList();
        }
        #endregion


        #region Post method
        // POST api/<DrugChangeController>
        [HttpPost]
        public string Post([FromBody] DrugChange value)
        {
            //checking if the user exist in the database

            if (!dbC.DrugChanges.Any(u => u.Id.Equals(value.Id)))
            {
                DrugChange user = new DrugChange();

                user.Id = value.Id;
                user.MedName = value.MedName;
                user.MedQuantity = value.MedQuantity;
                user.MedLocation = value.MedLocation;
                user.TimePrescribed = value.TimePrescribed;


                //Add to datbase
                try
                {
                    dbC.DrugChanges.Add(user);
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

            var query = dbC.DrugChanges
                .Where(s => s.MedLocation == MedLoc && s.TimePrescribed >= past30Days)
                .GroupBy(s => new
                {
                    MedName = s.MedName,
                    MedLocation = s.MedLocation,
                    PrescribedDay = s.TimePrescribed.Date // Still grouping by date (not formatted)
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
                DrugChange drug = dbC.DrugChanges.Find(Medname);
                if (drug != null)
                {
                    dbC.DrugChanges.Remove(drug);
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

        //#region get Med change details grouped by day with formatted date
        //// GET api/<UserLog>/location
        //[HttpGet("{MedLoc}")]
        //public IEnumerable<dynamic> GetPrescribedDrugsGrouped(string MedLoc)
        //{
        //    if (string.IsNullOrEmpty(MedLoc))
        //    {
        //        throw new ArgumentException("Medicine location must be provided.");
        //    }

        //    var past30Days = DateTime.Now.AddDays(-30);

        //    var query = dbC.DrugChanges
        //        .Where(s => s.MedLocation == MedLoc && s.TimePrescribed >= past30Days)
        //        .GroupBy(s => new
        //        {
        //            MedName = s.MedName,
        //            MedLocation = s.MedLocation,
        //            PrescribedDay = s.TimePrescribed.Date
        //        })
        //        .Select(group => new
        //        {
        //            MedName = group.Key.MedName,
        //            MedLocation = group.Key.MedLocation,
        //            PrescribedDay = group.Key.PrescribedDay.ToString("dd-MM-yyyy"), // Format date as dd-MM-yyyy
        //            TotalQuantity = group.Sum(s => s.MedQuantity) // Sum up quantities
        //        })
        //        .OrderBy(result => result.PrescribedDay) // Sort by formatted day
        //        .ToList();

        //    return query;
        //}
        //#endregion

        //#region get Med change details for the past 30 days days and time
        //// GET api/<UserLog>/location
        //[HttpGet("{MedLoc}")]
        //public IEnumerable<dynamic> GetPrescribedDrugs(string MedLoc)
        //{
        //    if (string.IsNullOrEmpty(MedLoc))
        //    {
        //        throw new ArgumentException("Medicine location must be provided.");
        //    }

        //    var past30Days = DateTime.Now.AddDays(-30);

        //    var query = dbC.DrugChanges
        //        .Where(s => s.MedLocation == MedLoc && s.TimePrescribed >= past30Days)
        //        .Select(s => new
        //        {
        //            MedName = s.MedName,
        //            MedLocation = s.MedLocation,
        //            TimePrescribed = s.TimePrescribed,
        //            MedQuantity = s.MedQuantity
        //        })
        //        .OrderBy(s => s.TimePrescribed) // Order by date of prescription
        //        .ToList();

        //    return query; // Return the list directly
        //}
        //#endregion
    }
}
