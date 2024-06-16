namespace CompanyEmployees.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Service.Contracts;
    using Shared.DataTransferObjects;

    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompanyController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:Guid}",Name = "CompanyId")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _serviceManager.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreation is null");

            var createdCompany = _serviceManager.CompanyService.CreateCompany(company);

            return CreatedAtRoute("CompanyId", new { id = createdCompany.Id }, createdCompany);
        }
    }
}
