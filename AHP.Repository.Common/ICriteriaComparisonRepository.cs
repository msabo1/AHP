﻿using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface ICriteriaComparisonRepository : IRepository<ICriteriaComparisonModel>
    {
        Task<List<ICriteriaComparisonModel>> GetByCriterionIDAsync(Guid criteriaID, int PageNumber, int PageSize = 5);
        Task<bool> DeleteByCriteriaIDAsync(Guid criteriaID);

    }
}
