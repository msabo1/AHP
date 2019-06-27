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
    class UserDeleteService : IUserDeleteService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        IUserRepository _userRepository;
        public UserDeleteService(IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteAsync(IUserModel user)
        {
            bool a=true;
            using (var uof = _unitOfWorkFactory.Create())
            {
                a = await _userRepository.DeleteAsync(user);
                await _userRepository.SaveAsync();
                uof.Commit();
            }
            return a;
        }

    }
}

