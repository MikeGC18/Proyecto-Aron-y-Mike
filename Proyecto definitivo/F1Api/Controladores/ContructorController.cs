using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1Api.Data;    
using F1Api.Models;

namespace FM1Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructorsController : ControllerBase
    {
        private readonly Fm1Context _context;
        private const string API_KEY = "TU_API_KEY";

        public ConstructorsController(Fm1Context context) => _context = context;

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Constructor>>> GetConstructors(
            int? constructorId, string? name, string? nationality)
        {
            var query = _context.Constructors.AsQueryable();

            if (constructorId.HasValue) query = query.Where(c => c.ConstructorId == constructorId.Value);
            if (!string.IsNullOrEmpty(name)) query = query.Where(c => c.Name.Contains(name));
            if (!string.IsNullOrEmpty(nationality)) query = query.Where(c => c.Nationality.Contains(nationality));

            return await query.ToListAsync();
        }

     
        [HttpPost]
        public async Task<ActionResult<Constructor>> PostConstructor(Constructor constructor, [FromHeader] string? api_key)
        {
            if (api_key != API_KEY) return Unauthorized("API_KEY inválida");

            _context.Constructors.Add(constructor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConstructors), new { id = constructor.ConstructorId }, constructor);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConstructor(int id, Constructor constructor, [FromHeader] string? api_key)
        {
            if (api_key != API_KEY) return Unauthorized("API_KEY inválida");
            if (id != constructor.ConstructorId) return BadRequest();

            _context.Entry(constructor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConstructor(int id, [FromHeader] string? api_key)
        {
            if (api_key != API_KEY) return Unauthorized("API_KEY inválida");

            var constructor = await _context.Constructors.FindAsync(id);
            if (constructor == null) return NotFound();

            _context.Constructors.Remove(constructor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

