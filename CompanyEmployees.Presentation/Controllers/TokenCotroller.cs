namespace CompanyEmployees.Presentation.Controllers
{
    using CompanyEmployees.Presentation.ActionFilters;
    using Microsoft.AspNetCore.Mvc;
    using Service.Contracts;
    using Shared.DataTransferObjects;

    [Route("api/token")]
    [ApiController]
    public class TokenCotroller : ControllerBase
    {
        private readonly IServiceManager _services;

        public TokenCotroller(IServiceManager services)
        {
            _services = services;
        }

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _services.AuthenticationServices.RefreshToken(tokenDto);

            return Ok(tokenDtoToReturn);
        }
    }
}
