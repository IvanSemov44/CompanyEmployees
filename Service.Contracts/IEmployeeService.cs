namespace Service.Contracts
{
    using Entities;
    using Entities.LinkModels;
    using Entities.Models;
    using Shared.DataTransferObjects;
    using Shared.RequestFeatures;
    using System.Dynamic;

    public interface IEmployeeService
    {
        Task<(LinkResponse linkResponse, MetaData? metaData)> GetEmployeesAsync(Guid companyId,
            LinkParameters linkParameters, bool trackChanges);
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
