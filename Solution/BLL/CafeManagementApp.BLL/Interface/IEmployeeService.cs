using CafeManagementApp.BLL.Model;
using DomainResults.Common;

namespace CafeManagementApp.BLL.Interface
{
    public interface IEmployeeService
    {
        // Employee CRUD
        Task<IDomainResult<EmployeeBll?>> UpsertEmployee(EmployeeBll employee);
        Task<IDomainResult<List<EmployeeBll>>> GetAllEmployees(string cafe);
        Task<IDomainResult<EmployeeBll?>> GetEmployeeByEmployeeId(long employeeId);
        Task<IDomainResult> DeleteEmployee(long employeeId);
    }
}
