﻿

using CyanCMS.Application.Interfaces;
using CyanCMS.Domain.Entities;
using CyanCMS.Utils.Common;
using CyanCMS.Utils.Request;
using Microsoft.AspNetCore.Mvc;

namespace CyanCMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyAppService _companyAppService;

        public CompanyController(ICompanyAppService companyAppService) 
        {
			_companyAppService = companyAppService;
        }

        [Route("GetAllCompany")]
        [HttpGet]
        public async Task<IActionResult> GetAllCompany([FromBody] RequestParams? @params)
        {
            string companyNameFilter = string.Empty;
            string companyIsActiveFilter = string.Empty;
            var queryParams = new CompanyParams();

            if (@params != null) {
                var attributeValues = DataQueryOperations.GetAttributeValues(@params.Filters);
                foreach (var attribute in attributeValues)
                {
                    if (attribute.Key == "companyName") {
                        companyNameFilter = attribute.Value;
                    }
                     else if (attribute.Key == "isActive")
                    {
                        companyIsActiveFilter = attribute.Value;
                    }
                }
                CompanyParams companyParams = new CompanyParams
                {
                    CompanyName = companyNameFilter,
                    IsActiveStr = companyIsActiveFilter,
                    PageSize = @params.PageSize,
                    PageNumber = @params.PageNumber,
                };
                queryParams = companyParams;
            }

            return Ok(await _companyAppService.GetAll(queryParams));
        }

        [Route("GetCompanyById")]
        [HttpPost]
        public async Task<IActionResult> GetCompanyById(string User_Pk)
        {
            return Ok(await _companyAppService.GetById(User_Pk));
        }

        [Route("CreateCompany")]
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] Company company)
        {
            if (company == null)
                return BadRequest();

			company.IsActive = true;
            company.IsDeleted = false;
			company.AuditCreateDate = DateTime.Now;

            await _companyAppService.Insert(company);
            return Created("Created", true);
        }

		[Route("UpdateCompany")]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] Company company)
		{
			if (company == null)
				return BadRequest();

			//company.Company_Id = new MongoDB.Bson.ObjectId(id);
            await _companyAppService.Update(company);
			return Created("Update", true);
		}

		[Route("DeleteCompany")]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] string id)
		{
			await _companyAppService.Delete(id);
			return NoContent();
		}
	}
}
