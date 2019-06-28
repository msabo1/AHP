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
        List<IAlternativeComparisonModel> AddAsync(List<IAlternativeComparisonModel> lista);
    }
}
