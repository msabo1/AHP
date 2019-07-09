﻿using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface ICriteriaComparisonService
    {

        Task<List<ICriteriaComparisonModel>> GetByCriteriaAsync(Guid criteriaId, int page = 1);
       
        Task<List<ICriteriaComparisonModel>> UpdateAsync(List<ICriteriaComparisonModel> comparisons);
    }
}
