using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CafeManagementApp.DAL.Shared.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CafeManagementApp.DAL.Shared.Service
{
    /// <summary>
    /// generic repository to be inherited
    /// </summary>
    /// <typeparam name="T">Database class object</typeparam>
    /// <typeparam name="T2">key object</typeparam>
    /// <typeparam name="T3">repository type</typeparam>
    public abstract class GenericRepository<T, T2, T3> : IGenericRepository<T, T2> where T : class
        where T3 : class
    {
        internal readonly DbContext _context;
        internal readonly DbSet<T> _dbSet;
        internal readonly Expression<Func<T, T2>> _getId;

        protected GenericRepository(DbContext context, Expression<Func<T, T2>> getId)
        {
            _context = context;
            _dbSet = context.Set<T>();
            this._getId = getId;
        }

        public virtual async Task<IEnumerable<T>> All(bool withTracking = false,
            params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return withTracking ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetById(T2 id, bool withTracking = false, 
            params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var propertyName = GetPropertyName();
            //below requires EFproperty to compile the code correctly when EF core converts it to SQL
            return withTracking 
                ? await query.FirstOrDefaultAsync(x => EF.Property<T2>(x, propertyName).Equals(id)) 
                : await query.AsNoTracking().FirstOrDefaultAsync(x => EF.Property<T2>(x, propertyName).Equals(id));
        }

        public async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public async Task<bool> AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return true;
        }

        public async Task<bool> Delete(T2 id)
        {
            //find by id
            var entity = await GetById(id);

            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            return true;
        }

        public async Task<bool> DeleteRange(T2[] ids)
        {            
            // Create stubs for each entity using the provided IDs
            foreach (var id in ids)
            {
                // Create a new instance of the entity and set its key property
                var entityStub = Activator.CreateInstance<T>();
                var propertyName = GetPropertyName();
                typeof(T).GetProperty(propertyName)?.SetValue(entityStub, id);

                // Mark the stub as Deleted in the DbContext
                _context.Entry(entityStub).State = EntityState.Deleted;
            }

            // Return true to indicate the entities are marked for deletion
            return true;
        }

        public virtual async Task<IEnumerable<T?>> Find(Expression<Func<T, bool>> predicate,
            bool withTracking = false,
            params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return withTracking
                ? await query.Where(predicate).ToListAsync()
                : await query.AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="getId"></param>
        /// <param name="setValuesAction">action to apply to existingEntity with new Entity</param>
        /// <returns></returns>
        public virtual async Task<T> Upsert(T entity, Action<T, T>? setValuesAction = null)
        {
            var existingEntity = await GetById(_getId.Compile()(entity));

            if (existingEntity != null)
            {
                var entry = _context.Entry(existingEntity);
                if (setValuesAction == null)
                {
                    entry.CurrentValues.SetValues(entity);
                }
                else
                {
                    setValuesAction(existingEntity, entity);
                }
            }
            else
            {
                await Add(entity);
            }

            return entity;
        }

        private string GetPropertyName()
        {
            // Use the correct property name from the _getId expression
            var propertyName = (_getId.Body as MemberExpression)?.Member.Name;
            if (propertyName == null)
            {
                throw new InvalidOperationException("The _getId expression must be a member expression.");
            }

            return propertyName;
        }
    }
}
