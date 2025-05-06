using CafeManagementApp.BLL.Interface;
using CafeManagementApp.BLL.Mapping;
using CafeManagementApp.BLL.Model;
using CafeManagementApp.DAL.Interface;
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
            var getCafeEmployees = await _unitOfWork.CafeRepository.GetById(cafeGuid, includes: x => x.CafeEmployees);

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

        public async Task<IDomainResult<List<CafeBll>>> GetAllCafes()
        {
            var cafeBlls = await _unitOfWork.CafeRepository.All(includes: x => x.CafeEmployees.Select(y => y.Employee));
            
            return DomainResult.Success(cafeBlls.Select(x => x.MapToBll()).ToList());
        }

        public async Task<IDomainResult<CafeBll?>> GetCafeByGuid(Guid cafeGuid)
        {
            var cafeBll = await _unitOfWork.CafeRepository.GetById(cafeGuid, 
                includes: x => x.CafeEmployees.Select(y => y.Employee));

            if (cafeBll == null)
            {
                return DomainResult.NotFound<CafeBll?>();
            }

            return DomainResult.Success(cafeBll.MapToBll());
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
