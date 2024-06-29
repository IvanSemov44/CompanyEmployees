namespace Service.Contracts
{
    using Microsoft.AspNetCore.Identity;
    using Shared.DataTransferObjects;
    public interface IAuthenticationServices
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userFotAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
