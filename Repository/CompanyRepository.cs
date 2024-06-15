namespace Repository
{
    using Contracts;
    using Entities;
    using System.Collections.Generic;

    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) {   }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
    }
}
