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

<<<<<<< HEAD:AHP.Service/User CRUD/UserLoginService.cs
        IUnitOfWork _unitOfWork;
        public UserLoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
=======
        public UserLogin(IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            UserRepository = userRepository;
        }

        public IUnitOfWorkFactory UnitOfWorkFactory { get; }
        public IUserRepository UserRepository { get; }
>>>>>>> d6bc07b5c5531bf5bdbcecbe7da6c0d09a023a00:AHP.Service/User CRUD/UserLogin.cs

        public async Task<IUserModel> Check(string username, string password)
        {
<<<<<<< HEAD:AHP.Service/User CRUD/UserLoginService.cs
            
            IUserModel user =  await _unitOfWork.UserRepository.GetByUsernameAsync(username);
=======

            IUserModel user = new UserModel { UserID = Guid.NewGuid(), Username = username, Password = password, DateCreated = DateTime.Now };
            using (var uof = UnitOfWorkFactory.Create())
            {
                UserRepository.Add(user);
                await UserRepository.SaveAsync();
                uof.Commit();
            }
>>>>>>> d6bc07b5c5531bf5bdbcecbe7da6c0d09a023a00:AHP.Service/User CRUD/UserLogin.cs

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
