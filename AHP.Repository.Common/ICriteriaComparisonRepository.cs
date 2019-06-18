using AHP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface ICriteriaComparisonRepository : IRepository<CriteriaComparisonModel>
    {
        Task<CriteriaComparisonModel> GetByIDsAsync(Guid id1, Guid id2);
    }
}
