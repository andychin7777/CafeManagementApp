using CafeManagementApp.BLL.Model;
using CafeManagementApp.Server.Model;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.Server.Mapping
{
    internal static class GetCafeViewModelMapping
    {
        internal static GetCafeViewModel? MapToGetViewModel(this CafeBll cafeBll)
        {
            if (cafeBll == null)
            {
                return null;
            }

            var returnValue = new GetCafeViewModel
            {
                Id = cafeBll.CafeGuid,
                Description = cafeBll.Description,
                Employees = cafeBll.CafeEmployees.LongCount(),
                Location = cafeBll.Location,
                Name = cafeBll.Name,
                Logo = cafeBll.Logo,
                
            };

            return returnValue;
        }
    }
}
