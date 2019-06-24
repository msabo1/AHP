using AHP.Model.Common;
using AHP.Repository.Common;
using AHP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    public class UserRegisterService : IUserRegisterService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        IUserRepository _userRepository;
        public UserRegisterService(IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userRepository = userRepository;
        }

        public async Task<IUserModel> Check(IUserModel user)
        {
           
                    IUserModel _user = await _userRepository.GetByUsernameAsync(user.Username);

                    if (_user != null)
                    {
                        return null;
                    }
                    else
                    {
                        user.UserID = Guid.NewGuid();
                        user.DateCreated = DateTime.Now;
                        user.DateUpdated = DateTime.Now;
                        using (var uof = _unitOfWorkFactory.Create())
                        {
                             user = _userRepository.Add(user);
                             await _userRepository.SaveAsync();
                             uof.Commit();
                        }
                        return user;
                    
                    }
                
            } 
        

    }
}
