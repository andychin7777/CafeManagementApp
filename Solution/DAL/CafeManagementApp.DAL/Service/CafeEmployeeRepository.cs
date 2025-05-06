using CafeManagementApp.DAL.Interface;
using CafeManagementApp.DAL.Shared.Service;
using CafeManagementApp.SQL.DBContext;
using CafeManagementApp.SQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagementApp.DAL.Service
{
    public class CafeEmployeeRepository : GenericRepository<CafeEmployee, long, CafeEmployeeRepository>, ICafeEmployeeRepository
    {
        public CafeEmployeeRepository(CafeManagementDBContext context)
            : base(context, x => x.CafeEmployeeId)
        {
        }
    }
}
