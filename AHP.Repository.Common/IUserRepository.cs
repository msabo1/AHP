using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHP.Model;

namespace AHP.Repository.Common
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetByIDAsync(Guid id);
        Task<UserModel> GetByUsernameAsync(string username);
        
    }
}