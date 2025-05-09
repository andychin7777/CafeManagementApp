using CafeManagementApp.BLL.Interface;
using CafeManagementApp.Server.Helper;
using CafeManagementApp.Server.Mapping;
using CafeManagementApp.Server.Model;
using DomainResults.Common;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagementApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(DomainResult), 500)]
    [ProducesResponseType(typeof(DomainResult), 400)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Retrieves a list of employees filtered by cafe
        /// </summary>
        /// <param name="cafe">The cafe to filter employees by (leave blank to ignore).</param>
        /// <returns>A list of employees in the specified cafe ordered by highest number of days worked first.</returns>
        [HttpGet]
        [Route("~/api/Employees")]
        [ProducesResponseType(typeof(List<GetEmployeeViewModel>), 200)]
        public async Task<IActionResult> GetEmployeeByCafe([FromQuery] string? cafe)
        {
            var result = await _employeeService.GetAllEmployees(cafe);
            return result.ToCustomReturnedActionResult(x =>
                x.Select(x => x.MapToGetViewModel()).OrderByDescending(x => x.DaysWorked).ToList(),
                this);
        }

        /// <summary>
        /// Create new employee, it should also create the linking records to the cafe 
        /// </summary>
        /// <param name="employeeViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeViewModel), 200)]
        public async Task<IActionResult> PostCreateEmployee([FromBody] EmployeeViewModel employeeViewModel)
        {
            //validate employeViewModel Employee Id is 0
            if (employeeViewModel.EmployeeId == 0)
            {
                return DomainResult.Failed($"{nameof(employeeViewModel.EmployeeId)} needs to be not 0 to update")
                    .ToCustomReturnedActionResult(this);
            }

            var result = await _employeeService.UpsertEmployee(employeeViewModel.MapToBll());
            return result.ToCustomReturnedActionResult(x => x.MapToViewModel(), this);
        }

        /// <summary>
        /// Update employee, it should also update the linking records to the cafe 
        /// </summary>
        /// <param name="employeeViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(EmployeeViewModel), 200)]
        public async Task<IActionResult> PutUpdateEmployee([FromBody] EmployeeViewModel employeeViewModel)
        {
            //validate employeViewModel Employee Id is 0
            if (employeeViewModel.EmployeeId == 0)
            {
                return DomainResult.Failed($"{nameof(employeeViewModel.EmployeeId)} needs to be not 0 to update")
                    .ToCustomReturnedActionResult(this);
            }

            var result = await _employeeService.UpsertEmployee(employeeViewModel.MapToBll());
            return result.ToCustomReturnedActionResult(x => x.MapToViewModel(), this);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(DomainResult), 200)]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int employeeId)
        {
            //validate employeViewModel Employee Id is 0
            if (employeeId == 0)
            {
                return DomainResult.Failed($"{nameof(employeeId)} needs to be not 0 to delete")
                    .ToCustomReturnedActionResult(this);
            }
            var result = await _employeeService.DeleteEmployee(employeeId);
            return result.ToCustomReturnedActionResult(this);
        }
    }
}
