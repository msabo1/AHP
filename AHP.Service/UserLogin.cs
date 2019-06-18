using AHP.Repository.Common;
using AHP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Model;

namespace AHP.Service
{
    class UserLogin : IUserLogin
    {
        IUserRepository _userRepo;
        string _password;
        string _username;

        public UserLogin(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<string> Check(string username, string password)
        {
            _username = username;
            _password = password;
            UserModel user = await _userRepo.GetByUsernameAsync(_username);

            if (user == null)
            {
                return "Username ne postoji";

            }
            else
            {
                if (user.Password == _password)
                {
                    return "User je legit";
                }
                else
                {
                    return "Kriva sifra";
                }
            }
        }

    }
}
