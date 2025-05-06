using CafeManagementApp.DAL.Shared.Interface;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.DAL.Interface
{
    public interface ICafeRepository : IGenericRepository<Cafe, Guid>
    {
    }
}