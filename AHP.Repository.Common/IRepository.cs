using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIDAsync(Guid id);
        Task<T> UpdateAsync(T oldEntity, T newEntity);
        Task<int> DeleteAsync(T entity);
    }
}
