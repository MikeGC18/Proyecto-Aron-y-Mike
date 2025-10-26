using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1Api.Data;    
using F1Api.Models;

namespace FM1Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircuitsController : ControllerBase
    {
        private readonly Fm1Context _context;
        private const string API_KEY = "TU_API_KEY"; 

        public CircuitsController(Fm1Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Circuit>>> GetCircuits(
            int? circuitId, string? name, string? location, string? country, double? lat, double? lng)
        {
            var query = _context.Circuits.AsQueryable();

            if (circuitId.HasValue) query = query.Where(c => c.CircuitId == circuitId.Value);
            if (!string.IsNullOrEmpty(name)) query = query.Where(c => c.Name.Contains(name));
            if (!string.IsNullOrEmpty(location)) query = query.Where(c => c.Location.Contains(location));
            if (!string.IsNullOrEmpty(country)) query = query.Where(c => c.Country.Contains(country));
            if (lat.HasValue) query = query.Where(c => c.Lat == lat.Value);
            if (lng.HasValue) query = query.Where(c => c.Lng == lng.Value);

            return await query.ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<Circuit>> PostCircuit(Circuit circuit, [FromHeader] string? api_key)
        {
            if (api_key != API_KEY) return Unauthorized("API_KEY inválida");

            _context.Circuits.Add(circuit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCircuits), new { id = circuit.CircuitId }, circuit);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCircuit(int id, Circuit circuit, [FromHeader] string? api_key)
        {
            if (api_key != API_KEY) return Unauthorized("API_KEY inválida");
            if (id != circuit.CircuitId) return BadRequest();

            _context.Entry(circuit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCircuit(int id, [FromHeader] string? api_key)
        {
            if (api_key != API_KEY) return Unauthorized("API_KEY inválida");

            var circuit = await _context.Circuits.FindAsync(id);
            if (circuit == null) return NotFound();

            _context.Circuits.Remove(circuit);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


