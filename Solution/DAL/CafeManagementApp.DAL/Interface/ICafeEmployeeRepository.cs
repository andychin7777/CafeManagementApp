using CafeManagementApp.DAL.Shared.Interface;
using CafeManagementApp.SQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagementApp.DAL.Interface
{
    public interface ICafeEmployeeRepository : IGenericRepository<CafeEmployee, long>
    {
    }
}
