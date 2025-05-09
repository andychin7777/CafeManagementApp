using CafeManagementApp.BLL.Model;
using CafeManagementApp.Server.Model;

namespace CafeManagementApp.Server.Mapping
{
    internal static class GetEmployeeViewModelMapping
    {
        internal static GetEmployeeViewModel? MapToGetViewModel(this EmployeeBll cafeBll)
        {
            if (cafeBll == null)
            {
                return null;
            }

            var returnValue = new GetEmployeeViewModel
            {
                Id = cafeBll.EmployeeIdString,
                EmailAddress = cafeBll.EmailAddress,
                Name = cafeBll.Name,
                Gender = cafeBll.Gender,
                PhoneNumber = cafeBll.PhoneNumber
            };

            //calculate days worked.
            //assume we dont need to worry about timezones
            var currentDateOnly = DateOnly.FromDateTime(DateTime.Now);
                        
            //get current cafe working at
            var currentCafeEmployeeRecord = cafeBll.CafeEmployees
                .Where(x => x.StartDate != null && x.StartDate <= currentDateOnly)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefault();

            if (currentCafeEmployeeRecord == null)
            {
                return returnValue;
            }
            
            returnValue.DaysWorked = currentDateOnly.DayNumber - currentCafeEmployeeRecord.StartDate!.Value.DayNumber;
            returnValue.Cafe = currentCafeEmployeeRecord?.Cafe?.Name;

            return returnValue;
        }
    }
}
