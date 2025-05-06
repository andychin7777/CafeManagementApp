using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagementApp.DAL.Interface
{
    public interface IGenericUnitOfWork
    {
        Task RunInTransaction(Func<Task> completeAction);
        Task<T> RunInTransaction<T>(Func<Task<T>> completeAction);
        Task SaveChanges();
    }
}
