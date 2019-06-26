using AHP.Model;
using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IAlternativeComparisonRepository : IRepository<IAlternativeComparisonModel>
    {
        Task<List<IAlternativeComparisonModel>> GetByCriteriaAlternativesID(Guid criteriaID, Guid alternativeID, int PageSize, int PageNumber);
        Task<bool> DeleteByAlternativeID(Guid alternativeID);
        Task<bool> DeleteByCriteriaID(Guid criteriaID);
    }
}
