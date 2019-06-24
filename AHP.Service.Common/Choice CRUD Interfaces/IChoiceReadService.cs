using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common.Choice_CRUD_Interfaces
{
    interface IChoiceReadService
    {
        Task<IChoiceModel> Check(string ChoiceName);
    }
}
