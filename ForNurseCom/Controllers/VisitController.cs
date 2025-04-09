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
    public class VisitController : ControllerBase
    {
        //the connection context
        private KMedicContext dbC = new KMedicContext();

        #region get all
        // GET: api/<VisitController>
        [HttpGet]
        public IEnumerable<Visit> Get()
        {
            return dbC.Visits.ToList();
        }
        #endregion

        //#region get all2
        //// GET: api/Visit/{PtID}
        //[HttpGet("{PtId}")]
        //public IEnumerable<Visit> GetAll(string PtId)
        //{
        //    var query = dbC.Visits.AsQueryable();

        //    if (!string.IsNullOrEmpty(PtId))
        //    {
        //       query = query.Where(d => d.PtId == PtId);
        //    }

        //    return query.ToList();
        //}
        //#endregion

        #region POst
        // POST api/<VisitController>
        [HttpPost]
        public String Post([FromBody] Visit value)
        {
            //checking if the user exist in the database

            if (!dbC.Visits.Any(u => u.VisitId.Equals(value.VisitId)))
            {
                Visit user = new Visit();

                user.VisitId = value.VisitId;
                user.CreatedAt = value.CreatedAt;
                user.PtId = value.PtId;
                user.PtName = value.PtName;
                user.PtAge = value.PtAge;
                user.PtWeight = value.PtWeight;
                user.PtHeight = value.PtHeight;
                user.PtBp = value.PtBp;
                user.PtHeart = value.PtHeart;
                user.PtTemp = value.PtTemp;
                user.PtDept = value.PtDept;
                user.PtResidence = value.PtResidence;
                user.PtLocation = value.PtLocation;
                user.PtNumber = value.PtNumber;
                user.VisitType = value.VisitType;
                user.PtImg = value.PtImg;
                user.Symptoms = value.Symptoms;
                user.BodySystem = value.BodySystem;
                user.EmerName = value.EmerName;
                user.EmerPhone = value.EmerPhone;
                user.EmerAddress = value.EmerAddress;
                user.Medicines = value.Medicines;
                user.MedQ = value.MedQ;
                user.MedicinesA = value.MedicinesA;
                user.MedQa = value.MedQa;
                user.MedicinesAb = value.MedicinesAb;
                user.MedQab = value.MedQab;
                user.Medicines4 = value.Medicines4;
                user.MedQ4 = value.MedQ4;
                user.Medicines5 = value.Medicines5;
                user.MedQ5 = value.MedQ5;
                user.NurseName = value.NurseName;




                //Add to datbase
                try
                {
                    dbC.Visits.Add(user);
                    dbC.SaveChanges();
                    return JsonConvert.SerializeObject("Information Save");
                }
                catch (Exception ex)
                {
                    //return JsonConvert.SerializeObject(ex.Message);
                    Console.WriteLine(ex.ToString()); // Log the full exception details
                    return JsonConvert.SerializeObject(ex.InnerException?.Message ?? ex.Message);
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Visit already exist try to update the value instead");
            }
        }
        #endregion

        #region get last 5 visits
        // GET: api/Visit/{PtID}
        [HttpGet("{PtId}")]
        public IEnumerable<Visit> GetLast5Visits(string PtId)
        {
            var query = dbC.Visits.AsQueryable();

            if (!string.IsNullOrEmpty(PtId))
            {
                query = query.Where(d => d.PtId == PtId);
            }

            // Order by descending based on the primary key or timestamp and take the last 5
            return query
                .OrderByDescending(d => d.CreatedAt) // Replace VisitDate with the appropriate sorting field
                .Take(5)
                .ToList();
        }
        #endregion


        #region GetByPatientId&VisitId
        // GET: api/Drug/{id}
        [HttpGet("{Ptid}/{id}")]
        public ActionResult<Drug> GetById(string Ptid, string Id)
        {
            var vis = dbC.Visits.FirstOrDefault(d => d.PtId == Ptid && d.VisitId == Id);

            if (vis == null)
            {
                return NotFound(); // Return 404 if the drug is not found
            }

            return Ok(vis); // Return the drug object with a 200 status code
        }
        #endregion

        #region Delete
        // DELETE api/<VisitController>/patientId
        [HttpDelete("{patientId}")]
        public String Delete(string patientId)
        {
            try
            {
                // Find all visits matching the given patient ID
                var visitsToDelete = dbC.Visits.Where(v => v.PtId.Equals(patientId)).ToList();

                // Check if any visits exist for the given patient ID
                if (visitsToDelete.Count == 0)
                {
                    return JsonConvert.SerializeObject($"No visits found for patient ID: {patientId}");
                }

                // Delete all matching visits from the database
                dbC.Visits.RemoveRange(visitsToDelete);
                dbC.SaveChanges();

                return JsonConvert.SerializeObject($"Successfully deleted all visits for patient ID: {patientId}");
            }
            catch (Exception ex)
            {
                // Log the full exception details
                Console.WriteLine(ex.ToString());
                return JsonConvert.SerializeObject(ex.InnerException?.Message ?? ex.Message);
            }
        }
        #endregion
    }
}
