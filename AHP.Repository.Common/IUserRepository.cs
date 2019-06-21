using System;
using System.Threading.Tasks;
using AHP.Model;

namespace AHP.Repository.Common
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetByUsernameAsync(string username);
    }
}