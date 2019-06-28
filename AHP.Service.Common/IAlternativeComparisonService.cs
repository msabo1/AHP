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
        Task<List<IAlternativeComparisonModel>> AddAsync(List<IAlternativeComparisonModel> list);
    }
}
