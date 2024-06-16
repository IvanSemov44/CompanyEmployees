namespace Service
{
    using AutoMapper;
    using Contracts;
    using global::Contracts;
    using Shared.DataTransferObjects;
    using System.Collections.Generic;

    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager logger,IMapper mapper) 
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
                var companies = _repository.Company.GetAllCompanies(trackChanges);

                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return companiesDto;
        }
    }
}
