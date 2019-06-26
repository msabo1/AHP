﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AHP.Model.Common;

namespace AHP.Service.Common.Choice_CRUD_Interfaces
{
    public interface IChoiceUpdateService
    {
        Task<IChoiceModel> Update(IChoiceModel choice);
    }
}
