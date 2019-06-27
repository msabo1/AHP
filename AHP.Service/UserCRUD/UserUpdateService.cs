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
        IUnitOfWorkFactory _unitOfWorkFactory;
        IUserRepository _userRepository;
        public UserUpdateService(IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userRepository = userRepository;
        }
       

        public async Task<IUserModel> UpdateAsync(IUserModel user)
        {
            
            IUserModel updated;
            var _baseUser = await _userRepository.GetByIDAsync(user.UserID);
            using (var uof = _unitOfWorkFactory.Create())
            {
                _baseUser.DateUpdated = DateTime.Now;
                if (user.Password != null) _baseUser.Password = user.Password;
                if (user.Username != null) _baseUser.Username = user.Username;
                updated = await _userRepository.UpdateAsync(_baseUser);
                await _userRepository.SaveAsync();
                uof.Commit();
            }
            return updated;
        }

    }
}
