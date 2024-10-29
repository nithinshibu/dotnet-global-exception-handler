using GlobalExceptionHandling.Models;
using GlobalExceptionHandling.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using GlobalExceptionHandling.Exceptions;

namespace GlobalExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("DriverList")]

        public async Task<IActionResult> DriverList()
        {
            var drivers = await _driverService.GetDrivers();
            return Ok(drivers);
        }

        [HttpPost("AddDriver")]
        public async Task<IActionResult> AddDriver(Driver driver)
        {
            var result = await _driverService.AddDriver(driver);
            return Ok(result);
        }

        [HttpGet("GetDriverById")]
        public async Task<IActionResult> GetDriverById(int id)
        {
            var result = await _driverService.GetDriverById(id);
            if(result == null)
            {
                //return NotFound();
                throw new NotFoundException("Invalid Id");
            }
            return Ok(result);
        }

        [HttpPost("UpdateDriver")]
        public async Task<IActionResult> UpdateDriver(Driver driver)
        {
            var result = await _driverService.UpdateDriver(driver);
            return Ok(result);
        }

    }
}
