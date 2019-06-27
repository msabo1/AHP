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
        public async Task<IUserModel> Check(string username, string password)
        {
            IUserModel user;
            using (var uof = _unitOfWorkFactory.Create())
            {
                user = await _userRepository.GetByUsernameAsync(username);
                user = await _userRepository.LoadChoicesPageAsync(user, 0);
                uof.Commit();
                
            }
            if (user != null)
            {

                if (user.Password == password)
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
