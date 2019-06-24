using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface ICriterionRepository : IRepository<ICriterionModel>
    {
        Task<List<ICriterionModel>> GetPageByChoiceID(Guid choiceID, int pageNumber, int pageSize);
    }
}
