using ForNurseCom.Models; using ForNurseCom.Data;
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
    public class DrugController : ControllerBase
    {
        //the connection context
        private KMedicContext dbC = new KMedicContext();

        #region get all
        // GET: api/<Drug>
        [HttpGet]

        public IEnumerable<Drug> Get()
        {
            return (IEnumerable<Drug>)dbC.Drugs.Where(d => d.MedQuantity > 0).ToList();
        }
        #endregion


        #region get all based on location
        // GET: api/Drug/{MedLocation}
        [HttpGet("{MedLocation}")]

        public IEnumerable<Drug> GetAll(string MedLocation)
        {
            var query = dbC.Drugs.AsQueryable();

            if (!string.IsNullOrEmpty(MedLocation))
            {
                query = query.Where(d => d.MedLocation == MedLocation);
            }

            return query.Where(d => d.MedQuantity > 0).ToList();
        }
        #endregion

        #region GetById
        // GET: api/Drug/{id}
        [HttpGet("{MedLocation}/{id}")]

        public ActionResult<Drug> GetById(string Id, string MedLocation)
        {
            var drug = dbC.Drugs.FirstOrDefault(d => d.Id == Id && d.MedLocation == MedLocation);

            if (drug == null)
            {
                return NotFound(); // Return 404 if the drug is not found
            }

            return Ok(drug); // Return the drug object with a 200 status code
        }
        #endregion


        #region POst
        // POST api/<Dreugss>
        [HttpPost]
        public String Post([FromBody] Drug value)
        {
            //checking if the user exist in the database

            if (!dbC.Drugs.Any(u => u.Id.Equals(value.Id)))
            {
                Drug user = new Drug();

                user.Id = value.Id;
                user.MedLocation = value.MedLocation;
                user.MedName = value.MedName;
                user.MedQuantity = value.MedQuantity;



                //Add to datbase
                try
                {
                    dbC.Drugs.Add(user);
                    dbC.SaveChanges();
                    return JsonConvert.SerializeObject($"{value.MedName} was added successfully");
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }
            }
            else
            {
                return JsonConvert.SerializeObject($"{value.MedName} already exist try to update the value instead");
            }
        }
        #endregion

     

        [AllowAnonymous]
        [HttpPut("{Id}")]
        public string Put(string Id, Drug value)
        {
            try
            {
                // Find the drug by ID
                var drug = dbC.Drugs.Find(Id);
                if (drug != null)
                {
                    // Update properties with incoming data
                    drug.MedName = value.MedName;
                    drug.MedLocation = value.MedLocation;

                    // Validate and update MedQuantity
                    if (value.MedQuantity > drug.MedQuantity)
                    {
                        return "Invalid quantity: insufficient stock.";
                    }
                    drug.MedQuantity = drug.MedQuantity - value.MedQuantity;
                    //drug.MedQuantity -= value.MedQuantity; 

                    // Mark entity as modified and save changes
                    dbC.Entry(drug).State = EntityState.Modified;
                    dbC.SaveChanges();

                    return $"{value.MedName} updated successfully.";
                }
                else
                {
                    return $"Medicine not found with ID: {Id}";
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }


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
