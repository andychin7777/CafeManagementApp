using CafeManagementApp.DAL.Interface;
using CafeManagementApp.DAL.Shared.Service;
using CafeManagementApp.SQL.DBContext;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.DAL.Service
{
    public class EmployeeRepository : GenericRepository<Employee, long, EmployeeRepository>, IEmployeeRepository
    {
        public EmployeeRepository(CafeManagementDBContext context)
            : base(context, x => x.EmployeeId)
        {
        }
    }
}
