using System;
using System.Threading.Tasks;
using AHP.Model;

namespace AHP.Repository.Common
{
    public interface IUserRepository
    {
        UserModel Add(UserModel user);
        bool Delete(UserModel user);
        Task<UserModel> GetByIDAsync(Guid id);
        Task<UserModel> GetByUsernameAsync(string username);
        Task<UserModel> UpdateAsync(UserModel user);
    }
}