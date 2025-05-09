using CafeManagementApp.BLL.Model;
using CafeManagementApp.Shared;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.BLL.Mapping
{
    internal static class EmployeeMapping
    {
        internal static Employee? MapToSql(this EmployeeBll employeeBll,
            Dictionary<string, object>? cache = null)
        {
            if (employeeBll == null)
            {
                return null;
            }

            var mappingCacheHelper = new MappingCacheHelper<EmployeeBll, Employee>(
                $"{employeeBll.EmployeeId}", ref cache);
            Func<Employee> mapFunction = () => new Employee
            {
                EmployeeId = employeeBll.EmployeeId,
                EmployeeIdString = employeeBll.EmployeeIdString,
                Name = employeeBll.Name,
                EmailAddress = employeeBll.EmailAddress,
                PhoneNumber = employeeBll.PhoneNumber,
                Gender = employeeBll.Gender
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var returnValue))
            {
                return returnValue;
            }

            returnValue.CafeEmployees = employeeBll.CafeEmployees.Select(x => x.MapToSql(cache: cache)).ToList();
            
            return returnValue;
        }

        internal static EmployeeBll? MapToBll(this Employee employee,
            Dictionary<string, object>? cache = null)
        {
            if (employee == null)
            {
                return null;
            }
            var mappingCacheHelper = new MappingCacheHelper<Employee, EmployeeBll>(
                $"{employee.EmployeeId}", ref cache);
            Func<EmployeeBll> mapFunction = () => new EmployeeBll
            {
                EmployeeId = employee.EmployeeId,
                EmployeeIdString = employee.EmployeeIdString,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var returnValue))
            {
                return returnValue;
            }

            returnValue.CafeEmployees = employee.CafeEmployees.Select(x => x.MapToBll(cache: cache)).ToList();
            
            return returnValue;
        }
    }
}
