using CafeManagementApp.BLL.Model;
using CafeManagementApp.Server.Model;
using CafeManagementApp.Shared;

namespace CafeManagementApp.Server.Mapping
{
    internal static class CafeEmployeeViewModelMapping
    {
        internal static CafeEmployeeBll? MapToBll(this CafeEmployeeViewModel cafeEmployeeViewModel, 
            Dictionary<string, object> cache = null)
        {
            if (cafeEmployeeViewModel == null)
            {
                return null;
            }
            var mappingCacheHelper = new MappingCacheHelper<CafeEmployeeViewModel, CafeEmployeeBll>(
                $"{cafeEmployeeViewModel.CafeEmployeeId}", ref cache);
            Func<CafeEmployeeBll> mapFunction = () => new CafeEmployeeBll
            {
                CafeEmployeeId = cafeEmployeeViewModel.CafeEmployeeId,
                CafeGuid = cafeEmployeeViewModel.CafeGuid,
                EmployeeId = cafeEmployeeViewModel.EmployeeId,
                StartDate = cafeEmployeeViewModel.StartDate,
                EndDate = cafeEmployeeViewModel.EndDate
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var cafeEmployeeBll))
            {
                return cafeEmployeeBll;
            }

            cafeEmployeeBll.Employee = cafeEmployeeViewModel.Employee?.MapToBll(cache: cache);
            cafeEmployeeBll.Cafe = cafeEmployeeViewModel.Cafe?.MapToBll(cache: cache);

            return cafeEmployeeBll;
        }

        internal static CafeEmployeeViewModel? MapToViewModel(this CafeEmployeeBll cafeEmployeeBll,
            Dictionary<string, object> cache = null)
        {
            if (cafeEmployeeBll == null)
            {
                return null;
            }
            var mappingCacheHelper = new MappingCacheHelper<CafeEmployeeBll, CafeEmployeeViewModel>(
                $"{cafeEmployeeBll.CafeEmployeeId}", ref cache);
            Func<CafeEmployeeViewModel> mapFunction = () => new CafeEmployeeViewModel
            {
                CafeEmployeeId = cafeEmployeeBll.CafeEmployeeId,
                CafeGuid = cafeEmployeeBll.CafeGuid,
                EmployeeId = cafeEmployeeBll.EmployeeId,
                StartDate = cafeEmployeeBll.StartDate,
                EndDate = cafeEmployeeBll.EndDate
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var cafeEmployeeViewModel))
            {
                return cafeEmployeeViewModel;
            }
            cafeEmployeeViewModel.Employee = cafeEmployeeBll.Employee?.MapToViewModel(cache: cache);
            cafeEmployeeViewModel.Cafe = cafeEmployeeBll.Cafe?.MapToViewModel(cache: cache);
            return cafeEmployeeViewModel;
        }
    }
}
