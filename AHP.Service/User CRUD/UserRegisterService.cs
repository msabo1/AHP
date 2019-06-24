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
        IUnitOfWork _unitOfWork;

        public UserRegisterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IUserModel> Check(IUserModel user)
        {
            IUserModel _user = await _unitOfWork.UserRepository.GetByUsernameAsync(user.Username);

            if (_user != null)
            {
                return null;
            }
            else
            {
                user.UserID = Guid.NewGuid();
                user.DateCreated = DateTime.Now;
                user.DateUpdated = DateTime.Now;
                user = _unitOfWork.UserRepository.Add(user);

                //Try catch nekakav ovdje za provjeru jel uspjela transakcija
                 await _unitOfWork.SaveAsync();
                 return user;
            }
        }

    }
}
