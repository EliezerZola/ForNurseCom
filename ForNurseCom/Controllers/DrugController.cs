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
    public class DrugController : ControllerBase
    {
        //the connection context
        private KmedicDbContext dbC = new KmedicDbContext();

        #region get all >0
        // GET: api/<Drug>
        [HttpGet]
        public IEnumerable<Drug> Get()
        {
            return (IEnumerable<Drug>)dbC.Drugs.Where(d => d.MedQuantity > 0).ToList();
        }
        #endregion

        #region get all including zero
        // GET: api/Drug/AllWithZero
        [HttpGet("AllWithZero")]
        public IEnumerable<Drug> GetAllIncludingZero()
        {
            return dbC.Drugs.ToList(); // no filtering on MedQuantity
        }
        #endregion


        #region get all including zero but with locAation flter
        // GET: api/Drug/AllWithZero
        [HttpGet("AllWithZero/{MedLocation}")]
        public IEnumerable<Drug> GetLocationIZero(string MedLocation)
        {
            var query = dbC.Drugs.AsQueryable();

            if (!string.IsNullOrEmpty(MedLocation))
            {
                query = query.Where(d => d.MedLocation == MedLocation);
            }

            return query.ToList();
        }
        #endregion

        #region get all by location
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

        //#region Update or Put
        //// PUT api/<Dreugss>/5
        //[HttpPut("{Id}")]
        //public string Put(string Id, Drug value)
        //{
        //    try
        //    {
        //        var drug = dbC.Drugs.FirstOrDefault(d => d.Id.Equals(Id, StringComparison.OrdinalIgnoreCase));

        //        if (drug != null)
        //        {
        //            drug.MedName = drug.MedName;
        //            drug.MedLocation = drug.MedLocation;

        //                drug.MedQuantity -= value.MedQuantity;
        //                dbC.Entry(drug).State = EntityState.Modified;
        //                dbC.SaveChanges();
        //                return $"{value.MedName} updated successfully";







        //        }
        //        else
        //        {
        //            return $"Medicine  No Found with this:" + (Id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }


        //}
        //#endregion


        #region Update to deduct medicine
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
        #endregion


        #region Add to existing medicine
        [HttpPut("AddOnlyQuantity/{Id}")]
        public IActionResult AddOnlyQuantity(string Id, [FromBody] int quantityToAdd)
        {
            try
            {
                var drug = dbC.Drugs.Find(Id);
                if (drug != null)
                {
                    if (quantityToAdd <= 0)
                    {
                        return BadRequest("Quantity to add must be greater than zero.");
                    }

                    drug.MedQuantity += quantityToAdd;

                    dbC.Entry(drug).State = EntityState.Modified;
                    dbC.SaveChanges();

                    return Ok($"{quantityToAdd} units successfully added to {drug.MedName}. New quantity: {drug.MedQuantity}");
                }
                else
                {
                    return NotFound($"Medicine not found with ID: {Id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

        #endregion


        #region Remove to existing medicine
        [HttpPut("MinusOnlyQuantity/{Id}")]
        public IActionResult MinusOnlyQuantity(string Id, [FromBody] int quantityToAdd)
        {
            try
            {
                var drug = dbC.Drugs.Find(Id);
                if (drug != null)
                {
                    if (quantityToAdd <= 0)
                    {
                        return BadRequest("Quantity to substract must be greater than zero.");
                    }

                    drug.MedQuantity -= quantityToAdd;

                    dbC.Entry(drug).State = EntityState.Modified;
                    dbC.SaveChanges();

                    return Ok($"{quantityToAdd} units successfully substracted to {drug.MedName}. New quantity: {drug.MedQuantity}");
                }
                else
                {
                    return NotFound($"Medicine not found with ID: {Id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
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
