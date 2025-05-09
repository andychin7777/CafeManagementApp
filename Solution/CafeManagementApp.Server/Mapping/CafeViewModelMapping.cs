using CafeManagementApp.BLL.Model;
using CafeManagementApp.Server.Model;
using CafeManagementApp.Shared;

namespace CafeManagementApp.Server.Mapping
{
    internal static class CafeViewModelMapping
    {
        internal static CafeBll? MapToBll(this CafeViewModel cafeViewModel, 
            Dictionary<string, object> cache = null)
        {
            if (cafeViewModel == null)
            {
                return null;
            }
            var mappingCacheHelper = new MappingCacheHelper<CafeViewModel, CafeBll>($"{cafeViewModel.CafeGuid}", ref cache);
            Func<CafeBll> mapFunction = () => new CafeBll
            {
                CafeGuid = cafeViewModel.CafeGuid,
                Name = cafeViewModel.Name!,
                Description = cafeViewModel.Description!,
                Logo = cafeViewModel.Logo,
                Location = cafeViewModel.Location!,
            };

            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var cafeBll))
            {
                return cafeBll;
            }

            return cafeBll;
        }

        internal static CafeViewModel? MapToViewModel(this CafeBll cafeBll,
            Dictionary<string, object> cache = null)
        {
            if (cafeBll == null)
            {
                return null;
            }

            var mappingCacheHelper = new MappingCacheHelper<CafeBll, CafeViewModel>($"{cafeBll.CafeGuid}", ref cache);
            Func<CafeViewModel> mapFunction = () => new CafeViewModel
            {
                CafeGuid = cafeBll.CafeGuid,
                Name = cafeBll.Name,
                Description = cafeBll.Description,
                Logo = cafeBll.Logo,
                Location = cafeBll.Location,
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var cafeViewModel))
            {
                return cafeViewModel;
            }
            return cafeViewModel;
        }
    }
}
