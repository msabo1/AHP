using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface ICriterionService
    {
        Task<ICriterionModel> AddAsync(ICriterionModel criterion);
        Task<bool> DeleteAsync(ICriterionModel criteria);
        Task<List<ICriterionModel>> GetPageAsync(Guid id, int page = 1);
        Task<List<ICriterionModel>> GetAllAsync(Guid id);
        Task<bool> UpdateAsync(ICriterionModel criteria);
    }
}