

using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager,ILoggerManager loggerManager,IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }
    }
}
