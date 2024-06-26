﻿using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthenticationServices> _authenticationServices;

        public ServiceManager
            (
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager,
            IMapper mapper,
            IEmployeeLinks employeeLinks,
            UserManager<User> userManager,
            IConfiguration configuration
            )
        {
            _companyService = new Lazy<ICompanyService>(
                () => new CompanyService(repositoryManager, loggerManager, mapper));
            _employeeService = new Lazy<IEmployeeService>(
                () => new EmployeeService(repositoryManager, loggerManager, mapper, employeeLinks));
            _authenticationServices = new Lazy<IAuthenticationServices>(
                () => new AuthenticationService(loggerManager, mapper, userManager, configuration));
        }
        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;

        public IAuthenticationServices AuthenticationServices => _authenticationServices.Value;
    }
}
