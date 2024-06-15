namespace Repository
{
    using Contracts;
    using Entities;

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            :base(repositoryContext) { }
    }
}
