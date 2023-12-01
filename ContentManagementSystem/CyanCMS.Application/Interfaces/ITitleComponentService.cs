﻿using CyanCMS.Domain.Entities;

namespace CyanCMS.Application.Interfaces
{
    public interface ITitleComponentService
	{
        Task Delete(string id);
        Task<IEnumerable<TitleComponent>> GetAll();
		Task<TitleComponent> GetById(string id);
		Task Insert(TitleComponent model);
        Task Update(TitleComponent model);
    }
}
