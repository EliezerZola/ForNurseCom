using ForNurseCom.ModelsMaria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ForNurseCom.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DrugSummaryController : ControllerBase
    {
        private KmedicDbContext dbC = new KmedicDbContext();

        // GET: api/<DrugSummaryController>

        [HttpGet("summary/{location}/{month}")]
        public async Task<ActionResult<IEnumerable<DrugSummaryDto>>> GetDrugSummaryByLocation(string location, int month)
        {
            // Step 1: Define month range for prescribed meds
            var targetMonth = month;
            var targetYear = DateTime.Now.Year;

            var startOfMonth = new DateTime(targetYear, targetMonth, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            // Step 2: Get available meds (no timestamp filtering)
            var available = await dbC.Drugs
                .Where(d => d.MedLocation == location)
                .GroupBy(d => new { d.MedName, d.MedLocation })
                .Select(g => new
                {
                    g.Key.MedName,
                    g.Key.MedLocation,
                    TotalAvailable = g.Sum(x => x.MedQuantity)
                }).ToListAsync();

            // Step 3: Get prescribed meds for the selected month
            var given = await dbC.Drugchanges
                .Where(dc => dc.MedLocation == location &&
                             dc.TimePrescribe >= startOfMonth &&
                             dc.TimePrescribe <= endOfMonth)
                .GroupBy(dc => new { dc.MedName, dc.MedLocation })
                .Select(g => new
                {
                    g.Key.MedName,
                    g.Key.MedLocation,
                    TotalGiven = g.Sum(x => x.MedQuantity)
                }).ToListAsync();

            // Step 4: Merge — start from available, fill in given or default to 0
            var summary = available
                .Select(a => new DrugSummaryDto
                {
                    MedName = a.MedName,
                    MedLocation = a.MedLocation,
                    TotalAvailable = a.TotalAvailable,
                    TotalGiven = given.FirstOrDefault(g => g.MedName == a.MedName)?.TotalGiven ?? 0
                })
                .ToList();

            return Ok(summary);
        }


        [HttpGet("monthlysum/{month}")]
        public async Task<IActionResult> GetMonthlyDrugSummary(int month)
        {
            var targetMonth = month;
            var targetYear = DateTime.Now.Year;

            var startOfMonth = new DateTime(targetYear, targetMonth, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);


            var available = await dbC.Drugs
     .GroupBy(d => d.MedName)
     .Select(g => new
     {
         MedName = g.Key,
         TotalAvailable = g.Sum(x => x.MedQuantity)
     }).ToListAsync();

            var given = await dbC.Drugchanges
                .Where(dc => dc.TimePrescribe >= startOfMonth &&
                             dc.TimePrescribe <= endOfMonth)
                .GroupBy(dc => dc.MedName)
                .Select(g => new
                {
                    MedName = g.Key,
                    TotalGiven = g.Sum(x => x.MedQuantity)
                }).ToListAsync();

            var summary = available
                .Select(a => new DrugSummaryDto
                {
                    MedName = a.MedName,
                    MedLocation = "All location",
                    TotalAvailable = a.TotalAvailable,
                    TotalGiven = given.FirstOrDefault(g => g.MedName == a.MedName)?.TotalGiven ?? 0
                }).ToList();


            #region comment
            //var startOfMonth = new DateTime(targetYear, targetMonth, 1);
            //var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            //// Step 1: Get available medicines (no date filter)
            //var available = await dbC.Drugs
            //    .GroupBy(d => new { d.MedName, d.MedLocation })
            //    .Select(g => new
            //    {
            //        g.Key.MedName,
            //        g.Key.MedLocation,
            //        TotalAvailable = g.Sum(x => x.MedQuantity)
            //    }).ToListAsync();

            //// Step 2: Get prescribed medicines for the month
            //var given = await dbC.Drugchanges
            //    .Where(dc => dc.TimePrescribe >= startOfMonth &&
            //                 dc.TimePrescribe <= endOfMonth)
            //    .GroupBy(dc => new { dc.MedName, dc.MedLocation })
            //    .Select(g => new
            //    {
            //        g.Key.MedName,
            //        g.Key.MedLocation,
            //        TotalGiven = g.Sum(x => x.MedQuantity)
            //    }).ToListAsync();

            //// Step 3: Merge — start from available, fill in given or default to 0
            //var summary = available
            //    .Select(a => new DrugSummaryDto
            //    {
            //        MedName = a.MedName,
            //        MedLocation = a.MedLocation,
            //        TotalAvailable = a.TotalAvailable,
            //        TotalGiven = given.FirstOrDefault(g =>
            //            g.MedName == a.MedName && g.MedLocation == a.MedLocation)?.TotalGiven ?? 0
            //    })
            //    .ToList(); 


            #endregion

            return Ok(summary);

        }


        [HttpGet("summary/all/{month}")]
        public async Task<ActionResult<IEnumerable<DrugSummaryDto>>> GetDrugSummaryAllLocations(int month)
        {
            var targetYear = DateTime.Now.Year;
            var startOfMonth = new DateTime(targetYear, month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            // Step 1: Aggregate available meds across all locations
            var available = await dbC.Drugs
                .GroupBy(d => d.MedName)
                .Select(g => new
                {
                    MedName = g.Key,
                    TotalAvailable = g.Sum(x => x.MedQuantity)
                }).ToListAsync();

            // Step 2: Aggregate prescribed meds for the selected month across all locations
            var given = await dbC.Drugchanges
                .Where(dc => dc.TimePrescribe >= startOfMonth &&
                             dc.TimePrescribe <= endOfMonth)
                .GroupBy(dc => dc.MedName)
                .Select(g => new
                {
                    MedName = g.Key,
                    TotalGiven = g.Sum(x => x.MedQuantity)
                }).ToListAsync();

            // Step 3: Merge by MedName
            var summary = available
                .Select(a => new DrugSummaryDto
                {
                    MedName = a.MedName,
                    MedLocation = "All Location",
                    TotalAvailable = a.TotalAvailable,
                    TotalGiven = given.FirstOrDefault(g => g.MedName == a.MedName)?.TotalGiven ?? 0
                }).ToList();

            return Ok(summary);
        }


    }
    }


