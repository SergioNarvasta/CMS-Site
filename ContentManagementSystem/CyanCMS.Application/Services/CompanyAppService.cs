﻿using CyanCMS.Application.Interfaces;
using CyanCMS.Domain.Entities;
using CyanCMS.Infraestructure.Interfaces;
using CyanCMS.Utils.Request;

namespace CyanCMS.Application.Services
{
    public class CompanyAppService : ICompanyAppService
    {
        public readonly ICompanyService _companyService;
        public CompanyAppService(ICompanyService companyService) {
           _companyService = companyService;
        }
        public async Task<bool> Delete(string id)
        {
            return await _companyService.Delete(id);
        }

        public async Task<IEnumerable<Company>> GetAll(CompanyParams @params)
        {
            return await _companyService.GetAll(@params);
        }

        public async Task<Company> GetById(string company_Pk)
        {
            return await _companyService.GetById(company_Pk);
        }

        public async Task<bool> Insert(Company model)
        {
            return await _companyService.Insert(model);
        }

        public async Task<bool> Update(Company model)
        {
            return await _companyService.Update(model);
        }
    }
}
