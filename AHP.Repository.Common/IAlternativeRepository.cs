using AHP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IAlternativeRepository : IRepository<AlternativeModel>
    {
        Task<AlternativeModel> GetByIDAsync(Guid id);
    }
}
