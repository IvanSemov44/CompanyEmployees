namespace Repository
{
    using Contracts;
    using Entities;

    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) {   }
    }
}
