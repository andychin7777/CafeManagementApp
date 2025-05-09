using CafeManagementApp.BLL.Model;
using CafeManagementApp.Server.Model;
using CafeManagementApp.Shared;

namespace CafeManagementApp.Server.Mapping
{
    internal static class EmployeeViewModelMapping
    {
        internal static EmployeeBll? MapToBll(this EmployeeViewModel employeeViewModel, 
            Dictionary<string, object> cache = null)
        {
            if (employeeViewModel == null)
            {
                return null;
            }

            var mappingCacheHelper = new MappingCacheHelper<EmployeeViewModel, EmployeeBll>(
                $"{employeeViewModel.EmployeeId}", ref cache);
            Func<EmployeeBll> mapFunction = () => new EmployeeBll
            {
                EmployeeId = employeeViewModel.EmployeeId,
                EmployeeIdString = employeeViewModel.EmployeeIdString,
                Name = employeeViewModel.Name,
                EmailAddress = employeeViewModel.EmailAddress,
                PhoneNumber = employeeViewModel.PhoneNumber,
                Gender = employeeViewModel.Gender
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var employeeBll))
            {
                return employeeBll;
            }

            employeeBll.CafeEmployees = employeeViewModel.CafeEmployees
                .Select(x => x.MapToBll(cache: cache)).ToList();

            return employeeBll;
        }

        internal static EmployeeViewModel? MapToViewModel(this EmployeeBll employeeBll, 
            Dictionary<string, object> cache = null)
        {
            if (employeeBll == null)
            {
                return null;
            }

            var mappingCacheHelper = new MappingCacheHelper<EmployeeBll, EmployeeViewModel>(
                $"{employeeBll.EmployeeId}", ref cache);
            Func<EmployeeViewModel> mapFunction = () => new EmployeeViewModel
            {
                EmployeeId = employeeBll.EmployeeId,
                EmployeeIdString = employeeBll.EmployeeIdString,
                Name = employeeBll.Name,
                EmailAddress = employeeBll.EmailAddress,
                PhoneNumber = employeeBll.PhoneNumber,
                Gender = employeeBll.Gender
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var employeeViewModel))
            {
                return employeeViewModel;
            }
            employeeViewModel.CafeEmployees = employeeBll.CafeEmployees
                .Select(x => x.MapToViewModel(cache: cache)).ToList();

            return employeeViewModel;
        }        
    }
}
