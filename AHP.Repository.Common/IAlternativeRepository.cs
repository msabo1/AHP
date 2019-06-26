using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IAlternativeRepository : IRepository<IAlternativeModel>
    {
        Task<List<IAlternativeModel>> GetAlternativesByChoiceID(Guid choiceID, int PageSize, int PageNumber);
    }
}
