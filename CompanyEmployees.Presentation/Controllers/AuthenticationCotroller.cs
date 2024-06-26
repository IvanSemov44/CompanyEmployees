namespace CompanyEmployees.Presentation.Controllers
{
    using CompanyEmployees.Presentation.ActionFilters;
    using Microsoft.AspNetCore.Mvc;
    using Service.Contracts;
    using Shared.DataTransferObjects;

    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationCotroller : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationCotroller(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationServices.RegisterUser(userForRegistration);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }
    }
}
