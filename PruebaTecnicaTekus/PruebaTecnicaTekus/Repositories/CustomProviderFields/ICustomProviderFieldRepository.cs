﻿using PruebaTecnicaTekus.Models;

namespace PruebaTecnicaTekus.Repositories.CustomProviderFields
{
    public interface ICustomProviderFieldRepository
    {
        Task<int> AddAsync(CustomProviderField customProviderField);
    }
}