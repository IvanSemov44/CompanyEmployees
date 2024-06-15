namespace CompanyEmployees.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Service.Contracts;

    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompanyController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
                return Ok(companies);
            }
            catch 
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
