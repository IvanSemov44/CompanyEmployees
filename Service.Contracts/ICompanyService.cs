namespace Service.Contracts
{
    using Shared.DataTransferObjects;

    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    }
}
