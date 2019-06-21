using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHP.Model;

namespace AHP.Repository.Common
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetByUsernameAsync(string username);
        Task<List<ChoiceModel>> GetChoices(Guid userID, int PageSize, int PageNumber);
    }
}