using CafeManagementApp.BLL.Interface;
using CafeManagementApp.BLL.Mapping;
using CafeManagementApp.BLL.Model;
using CafeManagementApp.BLL.Model.Validation;
using CafeManagementApp.DAL.Interface;
using DomainResults.Common;

namespace CafeManagementApp.BLL.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDomainResult> DeleteEmployee(long employeeId)
        {
            await _unitOfWork.EmployeeRepository.Delete(employeeId);
            await _unitOfWork.SaveChanges();
            return DomainResult.Success();
        }

        public async Task<IDomainResult<List<EmployeeBll>>> GetAllEmployees()
        {
            var employees = await _unitOfWork.EmployeeRepository.All(includes: x => x.CafeEmployees.Select(y => y.Cafe));
            return DomainResult.Success(employees.Select(x => x.MapToBll()).ToList());
        }

        public async Task<IDomainResult<EmployeeBll?>> GetEmployeeByEmployeeId(long employeeId)
        {
            var employee = await _unitOfWork.EmployeeRepository
                .GetById(employeeId, includes: x => x.CafeEmployees.Select(y => y.Cafe));

            if (employee == null)
            {
                return DomainResult.NotFound<EmployeeBll?>();
            }
            return DomainResult.Success(employee.MapToBll());
        }

        public async Task<IDomainResult<EmployeeBll?>> UpsertEmployee(EmployeeBll employee)
        {
            //validate employee first
            var validator = new EmployeeValidator();
            var validationResult = await validator.ValidateAsync(employee);
            if (!validationResult.IsValid)
            {
                return DomainResult.Failed<EmployeeBll?>(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            var updateSqlEmployee = employee.MapToSql();
            var isAdd = updateSqlEmployee.EmployeeId == 0;
            await _unitOfWork.EmployeeRepository.Upsert(updateSqlEmployee,
                (existingEntity, newEntity) =>
                {
                    existingEntity.Name = newEntity.Name;
                    existingEntity.EmailAddress = newEntity.EmailAddress;
                    existingEntity.PhoneNumber = newEntity.PhoneNumber;
                    existingEntity.Gender = newEntity.Gender;
                });

            if (!isAdd)
            {
                //update the related cafe employees
                var existingEmployee = await _unitOfWork.EmployeeRepository
                    .GetById(updateSqlEmployee.EmployeeId, includes: x => x.CafeEmployees);

                await UpdateRelatedEntities(
                    existingEmployee.CafeEmployees,
                    updateSqlEmployee.CafeEmployees,
                    (x, y) => x.CafeId == y.CafeId && x.StartDate == y.StartDate,
                    async entityToAdd => await _unitOfWork.CafeEmployeeRepository
                        .AddRange(entityToAdd),
                    async entityToDelete => await _unitOfWork.CafeEmployeeRepository
                        .DeleteRange(entityToDelete.Select(x => x.CafeEmployeeId).ToArray())
                );
            }

            await _unitOfWork.SaveChanges();
            return DomainResult.Success(updateSqlEmployee.MapToBll());
        }

        private async Task UpdateRelatedEntities<T>(
            IEnumerable<T> existingEntities,
            IEnumerable<T> newEntities,
            Func<T, T, bool> matchDelegate,
            Func<IEnumerable<T>, Task> addEntityAction,
            Func<IEnumerable<T>, Task> deleteEntityAction)
        {
            var listToDelete = existingEntities
                .Where(existing => !newEntities.Any(newEntity => matchDelegate(existing, newEntity)))
                .ToList();

            var listToAdd = newEntities
                .Where(newEntity => !existingEntities.Any(existing => matchDelegate(newEntity, existing)))
                .ToList();

            await deleteEntityAction(listToDelete);
            await addEntityAction(listToAdd);
        }
    }
}
