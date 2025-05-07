using CafeManagementApp.BLL.Model;
using DomainResults.Common;

namespace CafeManagementApp.BLL.Interface
{
    public interface ICafeService
    {
        // Cafe CRUD
        Task<IDomainResult<CafeBll?>> UpsertCafe(CafeBll cafe);
        Task<IDomainResult<List<CafeBll>>> GetAllCafes(string location);
        Task<IDomainResult<CafeBll?>> GetCafeByGuid(Guid cafeGuid);
        Task<IDomainResult> DeleteCafe(Guid cafeGuid);
    }
}
