namespace CafeManagementApp.DAL.Interface
{
    public interface IUnitOfWork : IGenericUnitOfWork, IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public ICafeEmployeeRepository CafeEmployeeRepository { get; }
        public ICafeRepository CafeRepository { get; }
    }
}
