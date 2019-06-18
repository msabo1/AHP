using AHP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface ICriterionRepository : IRepository<CriterionModel>
    {
        Task<CriterionModel> GetByIDAsync(Guid id);
    }
}
