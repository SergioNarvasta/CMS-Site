﻿
using CyanCMS.Domain.Entities;

namespace CyanCMS.Infraestructure.Interfaces
{
    public interface IPartnerService
	{
        Task Delete(string id);
        Task<IEnumerable<Partner>> GetAll();
		Task<Partner> GetById(string id);
		Task Insert(Partner model);
        Task Update(Partner model);
    }
}
