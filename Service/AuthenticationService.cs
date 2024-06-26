﻿namespace Service
{
    using AutoMapper;
    using Entities.Models;
    using global::Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Service.Contracts;
    using Shared.DataTransferObjects;

    internal sealed class AuthenticationService : IAuthenticationServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(ILoggerManager logger, IMapper mapper, UserManager<User> user,
            IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = user;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user,userForRegistration.PassWord);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return result;
        }
    }
}
