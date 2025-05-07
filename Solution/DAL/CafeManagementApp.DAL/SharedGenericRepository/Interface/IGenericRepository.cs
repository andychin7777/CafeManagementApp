using CafeManagementApp.DAL.Model;
using System.Linq.Expressions;

namespace CafeManagementApp.DAL.Shared.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Dbset object</typeparam>
    /// <typeparam name="T2">key</typeparam>
    public interface IGenericRepository<T, T2> where T : class
    {
        Task<IEnumerable<T>> All(bool withTracking = false, IncludesExpressionChain<T>[] includes = null);
        Task<T?> GetById(T2 id, bool withTracking = false, IncludesExpressionChain<T>[] includes = null);
        Task<bool> Add(T entity);
        Task<bool> AddRange(IEnumerable<T> entities);
        Task<bool> Delete(T2 id);
        Task<IEnumerable<T?>> Find(Expression<Func<T, bool>> predicate, bool withTracking = false, IncludesExpressionChain<T>[] includes = null);
        Task<T> Upsert(T entity, Action<T, T>? setValuesAction = null);
        Task<bool> DeleteRange(T2[] ids);
    }
}
