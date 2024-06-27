namespace Service
{
    using AutoMapper;
    using Entities.Models;
    using global::Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Service.Contracts;
    using Shared.DataTransferObjects;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    internal sealed class AuthenticationService : IAuthenticationServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User? _user;

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

            var result = await _userManager.CreateAsync(user, userForRegistration.PassWord);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.Username);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

            if (!result)
                _logger.LogWarn($"{nameof(ValidateUser)}: Authenticationf failed. Wrong username or password.");

            return result;
        }

        public async Task<string> CreateToken()
        {
            var singingCredentals = GetSingingCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(singingCredentals, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSingingCredentials()
        {
            var key = Encoding.UTF32.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,_user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSetting = _configuration.GetSection("JWTSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSetting["validIssuer"],
                audience: jwtSetting["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSetting["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}
