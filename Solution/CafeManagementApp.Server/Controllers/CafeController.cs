using CafeManagementApp.BLL.Interface;
using CafeManagementApp.BLL.Model;
using CafeManagementApp.Server.Helper;
using CafeManagementApp.Server.Mapping;
using CafeManagementApp.Server.Model;
using DomainResults.Common;
using DomainResults.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagementApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(DomainResult), 500)]
    [ProducesResponseType(typeof(DomainResult), 400)]
    public class CafeController : ControllerBase
    {
        private readonly ICafeService _cafeService;
        private readonly IEmployeeService _employeeService;

        public CafeController(ICafeService cafeService, IEmployeeService employeeService)
        {
            _cafeService = cafeService;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Retrieves a list of cafes filtered by location.
        /// </summary>
        /// <param name="location">The location to filter cafes by (leave blank to ignore).</param>
        /// <returns>A list of cafes in the specified location ordered by highest number of employees first.</returns>
        [HttpGet]
        [Route("~/api/cafes")]
        [ProducesResponseType(typeof(List<GetCafeViewModel>), 200)]
        public async Task<IActionResult> GetCafesByLocation([FromQuery] string? location)
        {
            var result = await _cafeService.GetAllCafes(location);
            return result.ToCustomReturnedActionResult(x => 
                x.Select(x => x.MapToViewModel()).OrderByDescending(x => x.Employees).ToList(), 
                this);
        }

        /// <summary>
        /// Retrieves a list of employees filtered by cafe
        /// </summary>
        /// <param name="cafe">The cafe to filter employees by (leave blank to ignore).</param>
        /// <returns>A list of employees in the specified cafe ordered by highest number of days worked first.</returns>
        [HttpGet]
        [Route("~/api/employees")]
        [ProducesResponseType(typeof(List<GetEmployeeViewModel>), 200)]
        public async Task<IActionResult> GetEmployeeByCafe([FromQuery] string? cafe)
        {
            var result = await _employeeService.GetAllEmployees(cafe);
            return result.ToCustomReturnedActionResult(x =>
                x.Select(x => x.MapToViewModel()).OrderByDescending(x => x.DaysWorked).ToList(),
                this);
        }
    }
}
