using Microsoft.AspNetCore.Mvc;
using F1API.Data;
using F1API.Models;

namespace F1API.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        private readonly F1Repository _repo;
        public DriversController(F1Repository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            return Ok(_repo.GetDrivers());
        }
    }
}
