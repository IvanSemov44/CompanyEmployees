﻿namespace CompanyEmployees.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Service.Contracts;

    [Route("api/companies")]
    [ApiController]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesV2Controller(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetComapnies()
        {
            var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);

            var companiesV2 = companies.Select(x => $"{x.Name} v2");

            return Ok(companiesV2);
        }
    }
}
