using AHP.Repository.Common;
using AHP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Model;
using AHP.Service.Common;
using AHP.Model.Common;

namespace AHP.Service
{
    class UserLogin : IUserLogin
    {
    

        public UserLogin(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<bool> Check(string username, string password)
        {
            
            IUserModel user =  await UnitOfWork.UserRepository.GetByUsernameAsync(username);

            if (user != null)
            {

                if(user.Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
