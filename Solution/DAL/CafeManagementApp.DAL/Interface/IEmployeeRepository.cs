using CafeManagementApp.DAL.Shared.Interface;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.DAL.Interface
{
    public interface IEmployeeRepository : IGenericRepository<Employee, long>
    {
    }
}
