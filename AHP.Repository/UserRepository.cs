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

        public async Task<UserModel> GetByIDAsync(params Guid[] idValues)
        {
            var user = await _context.Users.FindAsync(idValues);
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> UpdateAsync(UserModel user)
        {
            var _user = await _context.Users.FindAsync(user.UserID);
            _context.Entry(_user).CurrentValues.SetValues(_mapper.Map<UserModel, User>(user));
            return user; 
        }

        public bool Delete(UserModel user)
        {
            _context.Users.Remove(_mapper.Map<UserModel, User>(user));
            return true;
        }

        //ovo prebacis u Choices Repository ako bas zelis
        public async Task<List<ChoiceModel>> GetChoices(Guid userID, int PageSize, int PageNumber)
        {
            var choices = await _context.Choices.Where(c => c.UserID == userID).OrderBy(x => x.DateCreated).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            return _mapper.Map<List<Choice>, List<ChoiceModel>>(choices);      
        }

        public List<UserModel> AddRange(List<UserModel> users)
        {
            var _users = _mapper.Map<List<UserModel>, List<User>>(users);
            _context.Users.AddRange(_users);
            return users;
        }
    }
}
