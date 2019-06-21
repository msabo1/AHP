using AHP.Repository.Common;
using AHP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Model;

namespace AHP.Service
{
    class UserLogin : IUserLogin
    {
        string _password;
        string _username;

        public UserLogin(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<string> Check(string username, string password)
        {
            _username = username;
            _password = password;
            UserModel user = new UserModel { UserID = Guid.NewGuid(), Username = _username, Password = _password, DateCreated = DateTime.Now };
            UnitOfWork.UserRepository.Add(user);
            await UnitOfWork.SaveAsync();

            return username;
        }
    }
}
