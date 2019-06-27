using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.Common.CriterionCRUDInterfaces
{
    public interface ICriterionGetService
    {   
        Task<ICriterionModel> Get(ICriterionModel criteria);
    }
}
