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
            UserModel user =  await UnitOfWork.UserRepository.GetByUsernameAsync(username);
            user.Password = "345";
            UnitOfWork.UserRepository.UpdateAsync(user);
            await UnitOfWork.SaveAsync();

            if (user != null)
            {

                if(user.Password == password)
                {
                    return "ok";
                }
                else
                {
                    return "not ok";
                }
            }
            else
            {
                return "no user";
            }
        }
    }
}
