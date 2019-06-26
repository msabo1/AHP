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
        IUnitOfWorkFactory _unitOfWorkFactory;
        IUserRepository _userRepository;
        public UserLoginService(IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userRepository = userRepository;
        }
        public async Task<IUserModel> Check(IUserModel user)
        {
            
            var _user = await _userRepository.GetByUsernameAsync(user.Username);
  
            if (user != null)
            {

                if (_user.Password == user.Password)
                {
                    return _user;
                }
                else
                {
                    ;
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
