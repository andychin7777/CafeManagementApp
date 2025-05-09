using CafeManagementApp.BLL.Interface;
using CafeManagementApp.BLL.Mapping;
using CafeManagementApp.BLL.Model;
using CafeManagementApp.DAL.Interface;
using CafeManagementApp.DAL.Model;
using DomainResults.Common;

namespace CafeManagementApp.BLL.Service
{
    public class CafeService : ICafeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CafeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDomainResult> DeleteCafe(Guid cafeGuid)
        {
            //delete all employees in the cafe
            var includeModel = new[]
            {
                new IncludesExpressionChain<SQL.Model.Cafe>()
                {
                    Include = x => x.CafeEmployees
                }
            };
            var getCafeEmployees = await _unitOfWork.CafeRepository.GetById(cafeGuid, includes: includeModel);

            //delete link table
            await _unitOfWork.CafeEmployeeRepository.DeleteRange(getCafeEmployees.CafeEmployees
                .Select(x => x.CafeEmployeeId).ToArray());
            //delete employees
            await _unitOfWork.EmployeeRepository.DeleteRange(getCafeEmployees.CafeEmployees
                .Select(x => x.EmployeeId).ToArray());

            await _unitOfWork.CafeRepository.Delete(cafeGuid);
            await _unitOfWork.SaveChanges();

            return DomainResult.Success();
        }

        public async Task<IDomainResult<List<CafeBll>>> GetAllCafes(string location)
        {
            var includeModel = new []
            {   
                new IncludesExpressionChain<SQL.Model.Cafe>()
                {
                    Include = x => x.CafeEmployees,
                    ThenIncludes = [x => (x as SQL.Model.CafeEmployee).Employee]
                }
            };

            var cafes = 
                !string.IsNullOrEmpty(location) 
                ? await _unitOfWork.CafeRepository.Find(x => x.Location == location, 
                                includes: includeModel)
                : await _unitOfWork.CafeRepository.All(includes: includeModel);

            return DomainResult.Success(cafes.Select(x => x.MapToBll()).ToList());
        }

        public async Task<IDomainResult<CafeBll?>> GetCafeByGuid(Guid cafeGuid)
        {
            var includeModel = new[]
            {
                new IncludesExpressionChain<SQL.Model.Cafe>()
                {
                    Include = x => x.CafeEmployees,
                    ThenIncludes = [x => (x as SQL.Model.CafeEmployee).Employee]
                }
            };

            var cafes = await _unitOfWork.CafeRepository.GetById(cafeGuid, 
                includes: includeModel);

            if (cafes == null)
            {
                return DomainResult.NotFound<CafeBll?>();
            }

            return DomainResult.Success(cafes.MapToBll());
        }

        public async Task<IDomainResult<CafeBll?>> UpsertCafe(CafeBll cafe)
        {
            var sqlEntity = cafe.MapToSql();
            var updateResult = await _unitOfWork.CafeRepository.Upsert(sqlEntity,
                (existingEntity, newEntity) =>
                {
                    existingEntity.Name = newEntity.Name;
                    existingEntity.Description = newEntity.Description;
                    existingEntity.Logo = newEntity.Logo;
                    existingEntity.Location = newEntity.Location;
                });

            await _unitOfWork.SaveChanges();
            return DomainResult.Success(sqlEntity.MapToBll());
        }
    }
}
