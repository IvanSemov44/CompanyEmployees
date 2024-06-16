namespace Repository
{
    using Contracts;
    using Entities;

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            :base(repositoryContext) { }

        public IEnumerable<Employee> GetEmployees(Guid companyId,bool trackChanges)=>
            FindByCondition(e=>e.CompanyId.Equals(companyId),trackChanges)
            .ToList();
    }
}
