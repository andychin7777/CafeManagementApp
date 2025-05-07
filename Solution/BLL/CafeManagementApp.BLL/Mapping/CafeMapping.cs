using CafeManagementApp.BLL.Model;
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

            var key = $"{nameof(CafeBll)}_{cafeBll.CafeGuid}";
            cache ??= new Dictionary<string, object>();
            if (cache.TryGetValue(key, out var existingEntity))
            {
                return (Cafe)existingEntity;
            }

            var returnValue = new Cafe
            {
                CafeGuid = cafeBll.CafeGuid,
                Name = cafeBll.Name,
                Description = cafeBll.Description,
                Logo = cafeBll.Logo,
                Location = cafeBll.Location,
            };

            cache.Add(key, returnValue);

            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = cafeBll.CafeEmployees.Select(x => x.MapToSql(mapEmployee: true, cache: cache)).ToList();
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

            var key = $"{nameof(Cafe)}_{cafe.CafeGuid}";
            cache ??= new Dictionary<string, object>();
            if (cache.TryGetValue(key, out var existingEntity))
            {
                return (CafeBll)existingEntity;
            }

            var returnValue = new CafeBll
            {
                CafeGuid = cafe.CafeGuid,
                Name = cafe.Name,
                Description = cafe.Description,
                Logo = cafe.Logo,
                Location = cafe.Location,
            };

            cache.Add(key, returnValue);

            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = cafe.CafeEmployees.Select(x => x.MapToBll(mapEmployee: true, cache: cache)).ToList();
            }

            return returnValue;
        }
    }
}
