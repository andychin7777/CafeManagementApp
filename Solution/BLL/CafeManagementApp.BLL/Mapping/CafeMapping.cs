using CafeManagementApp.BLL.Model;
using CafeManagementApp.SQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagementApp.BLL.Mapping
{
    internal static class CafeMapping
    {
        internal static Cafe? MapToSql(this CafeBll cafeBll, bool mapCafeEmployees = false)
        {
            if (cafeBll == null)
            {
                return null;
            }

            var returnValue = new Cafe
            {
                CafeGuid = cafeBll.CafeGuid,
                Name = cafeBll.Name,
                Description = cafeBll.Description,
                Logo = cafeBll.Logo,
                Location = cafeBll.Location,
            };

            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = cafeBll.CafeEmployees.Select(x => x.MapToSql(mapEmployee: true)).ToList();
            }

            return returnValue;
        }

        internal static CafeBll? MapToBll(this Cafe cafe, bool mapCafeEmployees = false)
        {
            if (cafe == null)
            {
                return null;
            }

            var returnValue = new CafeBll
            {
                CafeGuid = cafe.CafeGuid,
                Name = cafe.Name,
                Description = cafe.Description,
                Logo = cafe.Logo,
                Location = cafe.Location,                
            };

            if (mapCafeEmployees)
            {
                returnValue.CafeEmployees = cafe.CafeEmployees.Select(x => x.MapToBll(mapEmployee: true)).ToList();
            }

            return returnValue;
        }
    }
}
