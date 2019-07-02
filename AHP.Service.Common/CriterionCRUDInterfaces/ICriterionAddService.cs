using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.Common.CriterionCRUDInterfaces
{
    public interface ICriterionAddService
    {
        Task<List<ICriterionModel>> AddAsync(List<ICriterionModel> criterion);
    }
}
