﻿using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IChoiceRepository : IRepository<IChoiceModel>
    {
        Task<List<IChoiceModel>> GetChoicesByUserID(Guid userID, int PageSize, int PageNumber);
    }
}
