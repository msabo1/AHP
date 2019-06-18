using AHP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IChoiceRepository : IRepository<ChoiceModel>
    {
        Task<List<ChoiceModel>> AddRangeAsync(List<ChoiceModel> choices);

        Task<ChoiceModel> GetByIDAsync(Guid id);
    }
}
