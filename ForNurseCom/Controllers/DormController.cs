
using ForNurseCom.ModelsMariaMaria;
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
    public class DormController : ControllerBase
    {
        //the connection context
        private KmedicDbContext dbC = new KmedicDbContext();

        #region get all
        // GET: api/<Staff>
        [HttpGet]
        public IEnumerable<Dorm> Get()
        {
            return dbC.Dorms.ToList();
        }
        #endregion

        #region gett by ID
        // GET api/<Staff>/5
        [HttpGet("{ContratctId}")]
        public Dorm Get(string ContratctId)
        {
            Dorm staff = dbC.Dorms.Find(ContratctId);
            if (staff != null)
            {
                return staff;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region POst
        // POST api/<Dreugss>
        [HttpPost]
        public String Post([FromBody] Dorm value)
        {
            //checking if the staff exist in the database

            if (!dbC.Dorms.Any(u => u.ContractId.Equals(value.ContractId)))
            {
                Dorm staff = new Dorm();

                staff.ContractId = value.ContractId;
                staff.GuestName = value.GuestName;
                staff.GuestId = value.GuestId;
                staff.Checkedin = value.Checkedin;
                staff.Room = value.Room;
                staff.RatepDay = value.RatepDay;
                staff.StayDuration = value.StayDuration;
                staff.Checkout = value.Checkout;
                staff.Cashortransfer = value.Cashortransfer;
                staff.Cashier = value.Cashier;
                staff.Totaltopay = value.RatepDay * value.StayDuration;




                //Add to datbase
                try
                {
                    dbC.Dorms.Add(staff);
                    dbC.SaveChanges();
                    return JsonConvert.SerializeObject("Guest Added");
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Guest already exists");
            }
        }
        #endregion

        #region Update or Put
        // PUT api/<Dreugss>/5
        [HttpPut("{ContactId}")]
        public string Put(string ContactId, Dorm value)
        {
            try
            {
                var staff = dbC.Dorms.Find(ContactId);
                if (staff != null)
                {
                    staff.ContractId = value.ContractId;
                    staff.GuestName = value.GuestName;
                    staff.GuestId = value.GuestId;
                    staff.Checkedin = value.Checkedin;
                    staff.Room = value.Room;
                    staff.RatepDay = value.RatepDay;
                    staff.StayDuration = value.StayDuration;
                    staff.Checkout = value.Checkout;
                    staff.Cashortransfer = value.Cashortransfer;
                    staff.Cashier = value.Cashier;
                    staff.Totaltopay = value.RatepDay * value.StayDuration;

                    dbC.Entry(staff).State = EntityState.Modified;
                    dbC.SaveChanges();
                    return "Guest info  updated successfully";
                }
                else
                {
                    return $"Guest Data Not Found:" + (ContactId);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
        #endregion

        #region Delete
        // DELETE api/<Dreugss>/5
        [HttpDelete("{Emp_ID}")]
        public string Delete(string Emp_ID)
        {
            try
            {
                Dorm staff = dbC.Dorms.Find(Emp_ID);
                if (staff != null)
                {
                    dbC.Dorms.Remove(staff);
                    dbC.SaveChanges();
                    return "Staff Data Deleted";
                }
                else
                {
                    return $"Guest Data Not found with  ID:" + (Emp_ID);
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
