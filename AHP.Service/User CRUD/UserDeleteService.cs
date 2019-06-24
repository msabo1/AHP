using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    class UserDeleteService : IUserDeleteService
    {
        IUnitOfWork _unitOfWork;

        public UserDeleteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Delete(IUserModel user)
        {
            return _unitOfWork.UserRepository.Delete(user);
        }

    }
}

