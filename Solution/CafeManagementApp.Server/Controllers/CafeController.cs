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
    public class CafeController : ControllerBase
    {
        private readonly ICafeService _cafeService;

        public CafeController(ICafeService cafeService, IEmployeeService employeeService)
        {
            _cafeService = cafeService;
        }

        /// <summary>
        /// Retrieves a list of cafes filtered by location.
        /// </summary>
        /// <param name="location">The location to filter cafes by (leave blank to ignore).</param>
        /// <returns>A list of cafes in the specified location ordered by highest number of employees first.</returns>
        [HttpGet]
        [Route("~/api/Cafes")]
        [ProducesResponseType(typeof(List<GetCafeViewModel>), 200)]
        public async Task<IActionResult> GetCafesByLocation([FromQuery] string? location)
        {
            var result = await _cafeService.GetAllCafes(location);
            return result.ToCustomReturnedActionResult(x =>
                x.Select(x => x.MapToGetViewModel()).OrderByDescending(x => x.Employees).ToList(),
                this);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CafeViewModel), 200)]
        public async Task<IActionResult> PostCreateCafe([FromBody] CafeViewModel cafeViewModel)
        {
            //validate the cafeViewModel guid value needs to be empty
            if (cafeViewModel.CafeGuid != Guid.Empty)
            {
                return DomainResult.Failed($"{nameof(cafeViewModel.CafeGuid)} needs to be empty to create")
                    .ToCustomReturnedActionResult(this);
            }

            var result = await _cafeService.UpsertCafe(cafeViewModel.MapToBll());
            return result.ToCustomReturnedActionResult(x => x.MapToViewModel(), this);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CafeViewModel), 200)]
        public async Task<IActionResult> PutUpdateCafe([FromBody] CafeViewModel cafeViewModel)
        {
            //validate the update guid value needs to be not empty
            if (cafeViewModel.CafeGuid == Guid.Empty)
            {
                return DomainResult.Failed($"{nameof(cafeViewModel.CafeGuid)} needs to be not empty to update")
                    .ToCustomReturnedActionResult(this);
            }

            var result = await _cafeService.UpsertCafe(cafeViewModel.MapToBll());
            return result.ToCustomReturnedActionResult(x => x.MapToViewModel(), this);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(DomainResult), 200)]
        public async Task<IActionResult> DeleteCafe([FromQuery] Guid cafeGuid)
        {
            //validate the cafeGuid value needs to be not empty
            if (cafeGuid == Guid.Empty)
            {
                return DomainResult.Failed($"{nameof(cafeGuid)} needs to be not empty to delete")
                    .ToCustomReturnedActionResult(this);
            }
            var result = await _cafeService.DeleteCafe(cafeGuid);
            return result.ToCustomReturnedActionResult(this);
        }
    }
}
