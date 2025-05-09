using CafeManagementApp.BLL.Model;
using CafeManagementApp.Shared;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.BLL.Mapping
{
    internal static class CafeEmployeeMapping
    {
        internal static CafeEmployee? MapToSql(this CafeEmployeeBll cafeEmployeeBll,            
            Dictionary<string, object>? cache = null)
        {
            if (cafeEmployeeBll == null)
            {
                return null;
            }

            var mappingCacheHelper = new MappingCacheHelper<CafeEmployeeBll, CafeEmployee>(
                $"{cafeEmployeeBll.CafeEmployeeId}", ref cache);
            Func<CafeEmployee> mapFunction = () => new CafeEmployee
            {
                CafeEmployeeId = cafeEmployeeBll.CafeEmployeeId,
                CafeGuid = cafeEmployeeBll.CafeGuid,
                EmployeeId = cafeEmployeeBll.EmployeeId,
                StartDate = cafeEmployeeBll.StartDate,
                EndDate = cafeEmployeeBll.EndDate
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var returnValue))
            {
                return returnValue;
            }

            returnValue.Employee = cafeEmployeeBll.Employee?.MapToSql();
            returnValue.Cafe = cafeEmployeeBll.Cafe?.MapToSql();
            return returnValue;
        }

        internal static CafeEmployeeBll? MapToBll(this CafeEmployee cafeEmployee,
            Dictionary<string, object>? cache = null)
        {
            if (cafeEmployee == null)
            {
                return null;
            }

            var mappingCacheHelper = new MappingCacheHelper<CafeEmployee, CafeEmployeeBll>(
                $"{cafeEmployee.CafeEmployeeId}", ref cache);
            Func<CafeEmployeeBll> mapFunction = () => new CafeEmployeeBll
            {
                CafeEmployeeId = cafeEmployee.CafeEmployeeId,
                CafeGuid = cafeEmployee.CafeGuid,
                EmployeeId = cafeEmployee.EmployeeId,
                StartDate = cafeEmployee.StartDate,
                EndDate = cafeEmployee.EndDate
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var returnValue))
            {
                return returnValue;
            }

            returnValue.Employee = cafeEmployee.Employee?.MapToBll(cache: cache);
            returnValue.Cafe = cafeEmployee.Cafe?.MapToBll(cache: cache);

            return returnValue;
        }
    }
}
