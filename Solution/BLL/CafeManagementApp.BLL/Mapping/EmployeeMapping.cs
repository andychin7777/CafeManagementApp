using CafeManagementApp.BLL.Model;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.BLL.Mapping
{
    internal static class EmployeeMapping
    {
        internal static Employee? MapToSql(this EmployeeBll employeeBll, bool mapCafeEmployees = false,
            Dictionary<string, object>? cache = null)
        {
            if (employeeBll == null)
            {
                return null;
            }

            var key = $"{nameof(EmployeeBll)}_{employeeBll.EmployeeId}";
            cache ??= new Dictionary<string, object>();
            if (cache.TryGetValue(key, out var existingEntity))
            {
                return (Employee)existingEntity;
            }

            var returnValue = new Employee
            {
                EmployeeId = employeeBll.EmployeeId,
                EmployeeIdString = employeeBll.EmployeeIdString,
                Name = employeeBll.Name,
                EmailAddress = employeeBll.EmailAddress,
                PhoneNumber = employeeBll.PhoneNumber,
                Gender = employeeBll.Gender                
            };

            cache.Add(key, returnValue);

            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = employeeBll.CafeEmployees.Select(x => x.MapToSql(mapCafe: true, cache: cache)).ToList();
            }
            return returnValue;
        }

        internal static EmployeeBll? MapToBll(this Employee employee, bool mapCafeEmployees = false,
            Dictionary<string, object>? cache = null)
        {
            if (employee == null)
            {
                return null;
            }

            var key = $"{nameof(Employee)}_{employee.EmployeeId}";
            cache ??= new Dictionary<string, object>();
            if (cache.TryGetValue(key, out var existingEntity))
            {
                return (EmployeeBll)existingEntity;
            }

            var returnValue = new EmployeeBll
            {
                EmployeeId = employee.EmployeeId,
                EmployeeIdString = employee.EmployeeIdString,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender                
            };

            cache.Add(key, returnValue);

            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = employee.CafeEmployees.Select(x => x.MapToBll(mapCafe: true, cache: cache)).ToList();
            }

            return returnValue;
        }
    }
}
