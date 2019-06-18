using AHP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IAlternativeComparisonRepository : IRepository<AlternativeComparisonModel>
    {
        Task<AlternativeComparisonModel> GetByIDsAsync(Guid idC, Guid idA1, Guid idA2);
    }
}
