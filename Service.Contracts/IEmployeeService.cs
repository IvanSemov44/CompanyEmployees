﻿namespace Service.Contracts
{
    using Entities;
    using Shared.DataTransferObjects;
    using Shared.RequestFeatures;

    public interface IEmployeeService
    {
        Task<(IEnumerable<EmployeeDto> employees, MetaData? metaData)> GetEmployeesAsync(Guid companyId,
            EmployeeParameters employeeParameters, bool trackChanges);
        Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation,
            bool trackChanges);

        Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChange);
        Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate,
            bool companyTrackChanges, bool employeeTrackChanges);

        Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
            Guid companyId, Guid id, bool compnanyTrackChanges, bool employeeTrackChanges);
        Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeForUpdate, Employee employeeEntity);
    }
}
