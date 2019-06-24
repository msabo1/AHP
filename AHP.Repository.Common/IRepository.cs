using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IRepository<T>
    {
        T Add(T entity);
        List<T> AddRange(List<T> entities);
        bool Delete(T entity);
        Task<T> GetByIDAsync(params Guid[] idValues);
        Task<T> UpdateAsync(T entity);
        Task<int> SaveAsync();
    }
}
