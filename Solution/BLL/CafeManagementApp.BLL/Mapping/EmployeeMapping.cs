using CafeManagementApp.BLL.Model;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.BLL.Mapping
{
    internal static class EmployeeMapping
    {
        internal static Employee? MapToSql(this EmployeeBll employeeBll, bool mapCafeEmployees = false)
        {
            if (employeeBll == null)
            {
                return null;
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
            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = employeeBll.CafeEmployees.Select(x => x.MapToSql(mapCafe: true)).ToList();
            }
            return returnValue;
        }

        internal static EmployeeBll? MapToBll(this Employee employee, bool mapCafeEmployees = false)
        {
            if (employee == null)
            {
                return null;
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

            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = employee.CafeEmployees.Select(x => x.MapToBll(mapCafe: true)).ToList();
            }

            return returnValue;
        }
    }
}
