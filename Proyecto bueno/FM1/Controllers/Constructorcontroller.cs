using Microsoft.AspNetCore.Mvc;
using F1API.Data;
using F1API.Models;

namespace F1API.Controllers
{
    [ApiController]
    [Route("api/constructors")]
    public class ConstructorsController : ControllerBase
    {
        private readonly F1Repository _repo;
        public ConstructorsController(F1Repository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetConstructors()
        {
            return Ok(_repo.GetConstructors());
        }
    }
}
