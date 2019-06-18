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
    class UserRepository
    {
        private AHPEntities _context;
        IMapper _mapper;
        public UserRepository(AHPEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserModel> AddAsync(UserModel user)
        {

            _context.Users.Add(_mapper.Map<UserModel, User>(user));
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<User>, List<UserModel>>(users);
        }

        public async Task<UserModel> GetByIDAsync(Guid id)
        {
            var user = await _context.Users.Where(u => u.UserID == id).FirstAsync();
            await _context.Entry(user).Collection(u => u.Choices).LoadAsync();
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstAsync();
            await _context.Entry(user).Collection(u => u.Choices).LoadAsync();
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> UpdateAsync(UserModel oldUser, UserModel newUser)
        {
            var _oldUser = _mapper.Map<UserModel, User>(oldUser);
            var user = await _context.Users.Where(u => u == _oldUser).FirstAsync();
            _context.Entry(user).CurrentValues.SetValues(newUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<int> DeleteAsync(UserModel user)
        {
            _context.Users.Remove(_mapper.Map<UserModel, User>(user));
            return await _context.SaveChangesAsync();
        }
    }
}
