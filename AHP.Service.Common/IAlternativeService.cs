using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface IAlternativeService
    {
        Task<IAlternativeModel> AddAsync(IAlternativeModel alternative);
        Task<bool> DeleteAsync(IAlternativeModel alternative);
        Task<List<IAlternativeModel>> GetPageAsync(Guid id, int page = 1);
        Task<List<IAlternativeModel>> GetAllAsync(Guid id);
        Task<IAlternativeModel> UpdateAsync(IAlternativeModel alternative);

    }
}
