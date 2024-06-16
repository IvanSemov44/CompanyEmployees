namespace Service.Contracts
{
    using Shared.DataTransferObjects;

    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
    }
}
