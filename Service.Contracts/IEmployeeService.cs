namespace Service.Contracts
{
    using Shared.DataTransferObjects;

    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
        EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges);
        EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation,
            bool trackChanges);

        void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChange);
        void UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate,
            bool companyTrackChanges, bool employeeTrackChanges);
    }
}
