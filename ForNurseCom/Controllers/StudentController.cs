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
    public class StudentController : ControllerBase
    {
        //the connection context
        private KmedicDbContext dbC = new KmedicDbContext();

        #region get all
        // GET: api/<Staff>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return dbC.Students.ToList();
        }
        #endregion

        #region gett by ID
        // GET api/<Student>/5
        [HttpGet("{Std_ID}")]
        public Student Get(string Std_ID)
        {
            Student staff = dbC.Students.Find(Std_ID);
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
        public String Post([FromBody] Student value)
        {
            //checking if the staff exist in the database

                if (!dbC.Students.Any(u => u.StdId.Equals(value.StdId)))
            {
                Student staff = new Student();

                staff.StdId = value.StdId;
                staff.StdName = value.StdName;
                staff.StdFac = value.StdFac;
                staff.StdImg = value.StdImg;



                //Add to datbase
                try
                {
                    dbC.Students.Add(staff);
                    dbC.SaveChanges();
                    return JsonConvert.SerializeObject("Student Added");
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Student already exist try to update the value instead");
            }
        }
        #endregion

        #region Update or Put
        // PUT api/<Dreugss>/5
        [HttpPut("{Std_ID}")]
        public string Put(string Std_ID, Student value)
        {
            try
            {
                var staff = dbC.Students.Find(Std_ID);
                if (staff != null)
                {
                    staff.StdName = value.StdName;
                    staff.StdFac = value.StdFac;
                    staff.StdImg = value.StdImg;

                    dbC.Entry(staff).State = EntityState.Modified;
                    dbC.SaveChanges();
                    return "drug Data updated successfully";
                }
                else
                {
                    return $"drug Data No Found:" + (Std_ID);
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
        [HttpDelete("{Std_ID}")]
        public string Delete(string Std_ID)
        {
            try
            {
                Student staff = dbC.Students.Find(Std_ID);
                if (staff != null)
                {
                    dbC.Students.Remove(staff);
                    dbC.SaveChanges();
                    return "Student Data Deleted";
                }
                else
                {
                    return $"Student Data Not found with  ID:" + (Std_ID);
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
