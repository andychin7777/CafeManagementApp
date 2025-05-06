using CafeManagementApp.BLL.Model;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.BLL.Mapping
{
    internal static class CafeEmployeeMapping
    {
        internal static CafeEmployee? MapToSql(this CafeEmployeeBll cafeEmployeeBll, 
            bool mapEmployee = false, 
            bool mapCafe = false)
        {
            if (cafeEmployeeBll == null)
            {
                return null;
            }

            var returnValue = new CafeEmployee
            {
                CafeEmployeeId = cafeEmployeeBll.CafeEmployeeId,
                CafeId = cafeEmployeeBll.CafeId,
                EmployeeId = cafeEmployeeBll.EmployeeId,
                StartDate = cafeEmployeeBll.StartDate
            };

            if (mapEmployee)
            {
                returnValue.Employee = cafeEmployeeBll.Employee?.MapToSql(mapCafeEmployees:true);
            }
            if (mapCafe)
            {
                returnValue.Cafe = cafeEmployeeBll.Cafe?.MapToSql(mapCafeEmployees: true);
            }
            return returnValue;
        }

        internal static CafeEmployeeBll? MapToBll(this CafeEmployee cafeEmployee,
            bool mapEmployee = false,
            bool mapCafe = false)
        {
            if (cafeEmployee == null)
            {
                return null;
            }

            var returnValue = new CafeEmployeeBll
            {
                CafeEmployeeId = cafeEmployee.CafeEmployeeId,
                CafeId = cafeEmployee.CafeId,
                EmployeeId = cafeEmployee.EmployeeId,
                StartDate = cafeEmployee.StartDate
            };

            if (mapEmployee)
            {
                returnValue.Employee = cafeEmployee.Employee?.MapToBll(mapCafeEmployees: true);
            }
            if (mapCafe)
            {
                returnValue.Cafe = cafeEmployee.Cafe?.MapToBll(mapCafeEmployees: true);
            }

            return returnValue;
        }
    }
}
