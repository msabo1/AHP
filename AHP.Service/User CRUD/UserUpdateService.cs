using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Service.Common;
namespace AHP.Service
{
    class UserUpdateService : IUserUpdateService
    {
        IUnitOfWork _unitOfWork;

        public UserUpdateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IUserModel> Update(IUserModel user)
        {
            return await _unitOfWork.UserRepository.UpdateAsync(user);
        }

    }
}
