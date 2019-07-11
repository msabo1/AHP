using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface IAlternativeComparisonService
    {

        Task<List<IAlternativeComparisonModel>> GetAsync(Guid criteriaId, Guid alternativeId, int page = 1);

        Task<List<IAlternativeComparisonModel>> UpdateAsync(List<IAlternativeComparisonModel> comparisons);
    }
}
