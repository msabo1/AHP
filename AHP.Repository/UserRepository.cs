using AHP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Repository.Common;
using System.Data.Entity;
using AHP.Model;

namespace AHP.Repository
{
    class UserRepository : IRepository<User>
    {
        private AHPEntities _context;

        public UserRepository(AHPEntities context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByIDAsync(Guid id)
        {
            var user = await _context.Users.Where(u => u.UserID == id).FirstAsync();
            await _context.Entry(user).Collection(u => u.Choices).LoadAsync();
            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstAsync();
            await _context.Entry(user).Collection(u => u.Choices).LoadAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User oldUser, User newUser)
        {
            var user = await _context.Users.Where(u => u == oldUser).FirstAsync();
            _context.Entry(user).CurrentValues.SetValues(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<int> DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync();
        }
    }
}
