using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1Api.Data;
using F1Api.Models;

namespace F1Api.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly Fm1Context _context;

        public DriversController(Fm1Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDrivers(
    [FromQuery] int? driverId,
    [FromQuery] int? number,
    [FromQuery] string? code,
    [FromQuery] string? forename,
    [FromQuery] string? surname,
    [FromQuery] string? dob,           // dob com a string
    [FromQuery] string? nationality)
        {
            var query = _context.Drivers.AsQueryable();

            if (driverId.HasValue) query = query.Where(d => d.DriverId == driverId.Value);
            if (number.HasValue) query = query.Where(d => d.Number == number.Value);
            if (!string.IsNullOrEmpty(code)) query = query.Where(d => d.Code == code);
            if (!string.IsNullOrEmpty(forename)) query = query.Where(d => d.Forename == forename);
            if (!string.IsNullOrEmpty(surname)) query = query.Where(d => d.Surname == surname);

            if (!string.IsNullOrEmpty(dob))
            {
                query = query.Where(d => d.Dob == dob);  // comparació directa com a string
            }

            if (!string.IsNullOrEmpty(nationality)) query = query.Where(d => d.Nationality == nationality);

            return Ok(query.ToList());
        }


        // POST: api/drivers
        [HttpPost]
        public IActionResult AddDriver([FromBody] Driver driver, [FromHeader(Name = "API_KEY")] string apiKey)
        {
            if (apiKey != "TU_API_KEY") return Unauthorized();

            _context.Drivers.Add(driver);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDrivers), new { driverId = driver.DriverId }, driver);
        }

        // PUT: api/drivers/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateDriver(int id, [FromBody] Driver driver, [FromHeader(Name = "API_KEY")] string apiKey)
        {
            if (apiKey != "TU_API_KEY") return Unauthorized();

            var existingDriver = _context.Drivers.Find(id);
            if (existingDriver == null) return NotFound();

            existingDriver.Number = driver.Number;
            existingDriver.Code = driver.Code;
            existingDriver.Forename = driver.Forename;
            existingDriver.Surname = driver.Surname;
            existingDriver.Dob = driver.Dob;
            existingDriver.Nationality = driver.Nationality;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/drivers/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(int id, [FromHeader(Name = "API_KEY")] string apiKey)
        {
            if (apiKey != "TU_API_KEY") return Unauthorized();

            var driver = _context.Drivers.Find(id);
            if (driver == null) return NotFound();

            _context.Drivers.Remove(driver);
            _context.SaveChanges();
            return NoContent();
        }
    }
}


