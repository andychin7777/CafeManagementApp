using CafeManagementApp.DAL.Interface;
using CafeManagementApp.DAL.Shared.Service;
using CafeManagementApp.SQL.DBContext;
using CafeManagementApp.SQL.Model;

namespace CafeManagementApp.DAL.Service
{
    public class CafeRepository : GenericRepository<Cafe, Guid, CafeRepository>, ICafeRepository
    {
        public CafeRepository(CafeManagementDBContext context) : base(context, x => x.CafeGuid)
        {
        }
    }
}
