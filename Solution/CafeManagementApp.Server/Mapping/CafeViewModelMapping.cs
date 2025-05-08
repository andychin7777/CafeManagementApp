using CafeManagementApp.BLL.Model;
using CafeManagementApp.Server.Model;

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

            //TODO: add the missing mapping cache helper
            return new CafeBll
            {
                CafeGuid = cafeViewModel.CafeGuid,
                Name = cafeViewModel.Name!,
                Description = cafeViewModel.Description!,
                Logo = cafeViewModel.Logo,
                Location = cafeViewModel.Location!,
            };
        }

        internal static CafeViewModel? MapToViewModel(this CafeBll cafeBll)
        {
            if (cafeBll == null)
            {
                return null;
            }

            //TODO: add the missing mapping cache helper
            return new CafeViewModel
            {
                CafeGuid = cafeBll.CafeGuid,
                Name = cafeBll.Name,
                Description = cafeBll.Description,
                Logo = cafeBll.Logo,
                Location = cafeBll.Location,
            };
        }
    }
}
