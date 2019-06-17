using AHP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Repository.Common;
using System.Data.Entity;

namespace AHP.Repository
{
    class UserRepository : IRepository<User>
    {
        private AHPEntities context;

        public UserRepository(AHPEntities _context)
        {
            context = _context;
        }
        public async Task<User> AddAsync(User user)
        {

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByIDAsync(Guid id)
        {
            var user = await context.Users.Where(u => u.UserID == id).FirstAsync();
            await context.Entry(user).Collection(u => u.Choices).LoadAsync();
            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await context.Users.Where(u => u.Username == username).FirstAsync();
            await context.Entry(user).Collection(u => u.Choices).LoadAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User oldUser, User newUser)
        {
            var user = await context.Users.Where(u => u == oldUser).FirstAsync();
            context.Entry(user).CurrentValues.SetValues(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }

        public async Task<int> DeleteAsync(User user)
        {
            context.Users.Remove(user);
            return await context.SaveChangesAsync();
        }
    }
}
