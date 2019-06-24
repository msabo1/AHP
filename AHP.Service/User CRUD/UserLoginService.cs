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
    class UserLoginService : IUserLoginService
    {

        IUnitOfWork _unitOfWork;
        public UserLoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        

        public async Task<IUserModel> Check(string username, string password)
        {
            
            IUserModel user =  await _unitOfWork.UserRepository.GetByUsernameAsync(username);

            if (user != null)
            {

                if(user.Password == password)
                {
                    return user;
                }
                else
                {
                    user = null;
                    return user;
                }
            }
            else
            {
                return user;
            }
        }
    }
}
