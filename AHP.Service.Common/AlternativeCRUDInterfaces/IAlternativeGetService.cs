using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IAlternativeGetService
    {
        Task<List<IAlternativeModel>> GetAsync(Guid id, int page = 1);
    }
}