using CafeManagementApp.DAL.Interface;
using CafeManagementApp.SQL.DBContext;

namespace CafeManagementApp.DAL.Service.Generic
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CafeManagementDBContext _dbContext;

        public ICafeRepository CafeRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public ICafeEmployeeRepository CafeEmployeeRepository { get; }

        public UnitOfWork(CafeManagementDBContext dbContext,
            ICafeRepository cafeRepository,
            IEmployeeRepository employeeRepository,
            ICafeEmployeeRepository cafeEmployeeRepository)
        {
            _dbContext = dbContext;
            CafeRepository = cafeRepository;
            EmployeeRepository = employeeRepository;
            CafeEmployeeRepository = cafeEmployeeRepository;
        }

        public async Task RunInTransaction(Func<Task> completeAction)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            await completeAction();
            await transaction.CommitAsync();
        }
        public async Task<T> RunInTransaction<T>(Func<Task<T>> completeAction)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            var result = await completeAction();
            await transaction.CommitAsync();

            return result;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
