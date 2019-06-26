using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHP.Model;
using AHP.Model.Common;

namespace AHP.Repository.Common
{
    public interface IUserRepository : IRepository<IUserModel>
    {

        Task<IUserModel> GetByUsernameAsync(string username);
        Task<IUserModel> LoadChoicesPage(IUserModel user, int PageNumber, int PageSize = 5);

    }
}