using Microsoft.AspNetCore.Mvc;
using F1API.Data;
using F1API.Models;

namespace F1API.Controllers
{
    [ApiController]
    [Route("api/circuits")]
    public class CircuitsController : ControllerBase
    {
        private readonly F1Repository _repo;
        public CircuitsController(F1Repository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetCircuits()
        {
            return Ok(_repo.GetCircuits());
        }
    }
}
