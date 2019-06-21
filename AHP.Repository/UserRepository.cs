using AHP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Repository.Common;
using System.Data.Entity;
using AHP.Model;
using AutoMapper;

namespace AHP.Repository
{

    public class UserRepository : IUserRepository

    {
        private AHPEntities _context;
        IMapper _mapper;
        public UserRepository(AHPEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public UserModel Add(UserModel user)
        {

            _context.Users.Add(_mapper.Map<UserModel, User>(user));
            return user;
        }

        public async Task<UserModel> GetByIDAsync(Guid id)
        {
            var user = await _context.Users.Where(u => u.UserID == id).FirstAsync();
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            return _mapper.Map<User, UserModel>(user);
        }

        public UserModel Update(UserModel user)
        {
            var _user = _mapper.Map<UserModel, User>(user);
            _context.Users.Attach(_user);
            _context.Entry(_user).State = EntityState.Modified;
            return user; 
        }

        public bool Delete(UserModel user)
        {
            _context.Users.Remove(_mapper.Map<UserModel, User>(user));
            return true;
        }
    }
}
