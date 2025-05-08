using CafeManagementApp.BLL.Model;
using CafeManagementApp.Shared;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.BLL.Mapping
{
    internal static class CafeMapping
    {
        internal static Cafe? MapToSql(this CafeBll cafeBll, bool mapCafeEmployees = false, 
            Dictionary<string, object>? cache = null)
        {
            if (cafeBll == null)
            {
                return null;
            }

            var mappingCacheHelper = new MappingCacheHelper<CafeBll, Cafe>($"{cafeBll.CafeGuid}", ref cache);
            Func<Cafe> mapAction = () =>
            {
                var mappedItem = new Cafe
                {
                    CafeGuid = cafeBll.CafeGuid,
                    Name = cafeBll.Name,
                    Description = cafeBll.Description,
                    Logo = cafeBll.Logo,
                    Location = cafeBll.Location,
                };
                return mappedItem;
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapAction, out var returnValue))
            {
                return returnValue;
            }

            //additional mappings
            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = cafeBll.CafeEmployees
                    .Select(x => x.MapToSql(mapEmployee: true, cache: cache)).ToList();
            }

            return returnValue;
        }

        internal static CafeBll? MapToBll(this Cafe cafe, bool mapCafeEmployees = false, 
            Dictionary<string, object>? cache = null)
        {
            if (cafe == null)
            {
                return null;
            }

            var mappingCacheHelper = new MappingCacheHelper<Cafe, CafeBll>($"{cafe.CafeGuid}", ref cache);
            Func<CafeBll> mapFunction = () => new CafeBll
            {
                CafeGuid = cafe.CafeGuid,
                Name = cafe.Name,
                Description = cafe.Description,
                Logo = cafe.Logo,
                Location = cafe.Location,
            };
            if (mappingCacheHelper.TryGetExistingEntityElseMap(mapFunction, out var returnValue))
            {
                return returnValue;
            }

            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = cafe.CafeEmployees.Select(x => x.MapToBll(mapEmployee: true, cache: cache)).ToList();
            }

            return returnValue;
        }
    }
}
