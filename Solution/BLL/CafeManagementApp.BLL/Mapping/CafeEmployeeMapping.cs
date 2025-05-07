using CafeManagementApp.BLL.Model;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.BLL.Mapping
{
    internal static class CafeEmployeeMapping
    {
        internal static CafeEmployee? MapToSql(this CafeEmployeeBll cafeEmployeeBll, 
            bool mapEmployee = false, 
            bool mapCafe = false,
            Dictionary<string, object>? cache = null)
        {
            if (cafeEmployeeBll == null)
            {
                return null;
            }

            var key = $"{nameof(CafeEmployeeBll)}_{cafeEmployeeBll.CafeEmployeeId}";
            cache ??= new Dictionary<string, object>();
            if (cache.TryGetValue(key, out var existingEntity))
            {
                return (CafeEmployee)existingEntity;
            }

            var returnValue = new CafeEmployee
            {
                CafeEmployeeId = cafeEmployeeBll.CafeEmployeeId,
                CafeGuid = cafeEmployeeBll.CafeGuid,
                EmployeeId = cafeEmployeeBll.EmployeeId,
                StartDate = cafeEmployeeBll.StartDate,
                EndDate = cafeEmployeeBll.EndDate
            };

            cache.Add(key, returnValue);

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
            bool mapCafe = false,
            Dictionary<string, object>? cache = null)
        {
            if (cafeEmployee == null)
            {
                return null;
            }

            var key = $"{nameof(CafeEmployee)}_{cafeEmployee.CafeEmployeeId}";
            cache ??= new Dictionary<string, object>();
            if (cache.TryGetValue(key, out var existingEntity))
            {
                return (CafeEmployeeBll)existingEntity;
            }

            var returnValue = new CafeEmployeeBll
            {
                CafeEmployeeId = cafeEmployee.CafeEmployeeId,
                CafeGuid = cafeEmployee.CafeGuid,
                EmployeeId = cafeEmployee.EmployeeId,
                StartDate = cafeEmployee.StartDate,
                EndDate = cafeEmployee.EndDate
            };

            cache.Add(key, returnValue);

            if (mapEmployee)
            {
                returnValue.Employee = cafeEmployee.Employee?.MapToBll(mapCafeEmployees: true, cache: cache);
            }
            if (mapCafe)
            {
                returnValue.Cafe = cafeEmployee.Cafe?.MapToBll(mapCafeEmployees: true, cache: cache);
            }

            return returnValue;
        }
    }
}
