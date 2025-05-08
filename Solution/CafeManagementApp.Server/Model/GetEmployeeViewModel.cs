namespace CafeManagementApp.Server.Model
{
    public class GetEmployeeViewModel
    {
        /// <summary>
        /// Unique employee identifier in the format ‘UIXXXXXXX’ where the X is 
        /// replaced with alpha numeric
        /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        /// <summary>
        /// Number of days the employee worked
        /// </summary>
        public long DaysWorked { get; set; }
        /// <summary>
        /// Café’s name that the employee is under [leave blank if not assigned yet]
        /// </summary>
        public string Cafe { get; set; }
    }
}
