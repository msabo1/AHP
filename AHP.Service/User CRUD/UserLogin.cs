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
    class UserLogin : IUserLogin
    {
    

        public UserLogin(IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            UserRepository = userRepository;
        }

        public IUnitOfWorkFactory UnitOfWorkFactory { get; }
        public IUserRepository UserRepository { get; }

        public async Task<bool> Check(string username, string password)
        {

            IUserModel user = new UserModel { UserID = Guid.NewGuid(), Username = username, Password = password, DateCreated = DateTime.Now };
            using (var uof = UnitOfWorkFactory.Create())
            {
                UserRepository.Add(user);
                await UserRepository.SaveAsync();
                uof.Commit();
            }

            if (user != null)
            {

                if(user.Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
