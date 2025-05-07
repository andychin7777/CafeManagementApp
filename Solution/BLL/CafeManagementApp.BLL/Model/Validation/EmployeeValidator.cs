using FluentValidation;

namespace CafeManagementApp.BLL.Model.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeBll>
    {
        // Private field to store the error message
        private string _overlapErrorMessage = string.Empty;

        public EmployeeValidator()
        {
            RuleFor(x => x.CafeEmployees)
                .Must(BeUniqueCafeEmployeePeriod)
                .WithMessage(x => _overlapErrorMessage); // Use the private field for the error message
        }

        private bool BeUniqueCafeEmployeePeriod(IList<CafeEmployeeBll> cafeEmployees)
        {
            // Sort the cafeEmployees by CafeId, then by StartDate
            var sortedCafeEmployees = cafeEmployees
                .OrderBy(x => x.CafeGuid)
                .ThenBy(x => x.StartDate)
                .ToList();

            // List to store all overlap messages
            var overlapMessages = new List<string>();

            // Check for overlapping date ranges
            for (int i = 0; i < sortedCafeEmployees.Count - 1; i++)
            {
                var current = sortedCafeEmployees[i];
                var next = sortedCafeEmployees[i + 1];

                // Check for overlap
                if (current.EndDate.HasValue &&
                    next.StartDate.HasValue &&
                    current.EndDate >= next.StartDate)
                {
                    // Add overlap details to the list
                    overlapMessages.Add(
                        $"Overlap detected: CafeId {current.CafeGuid} with range " +
                        $"{current.StartDate} - {current.EndDate} " +
                        $"overlaps with CafeId {next.CafeGuid} {next.StartDate} - {next.EndDate}.");
                }
            }

            // If overlaps exist, update the private error message and return false
            if (overlapMessages.Any())
            {
                _overlapErrorMessage = string.Join(" ", overlapMessages);
                return false;
            }

            // Clear the error message if no overlaps are found
            _overlapErrorMessage = string.Empty;
            return true;
        }
    }
}
