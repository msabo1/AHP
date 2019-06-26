using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface ICriteriaComparisonRepository : IRepository<ICriteriaComparisonModel>
    {
        Task<List<ICriteriaComparisonModel>> GetCriteriaComparisonsByCriterionID(Guid criteriaID, int PageSize, int PageNumber);

    }
}
