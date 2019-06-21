using AHP.DAL;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; set; }
        private AHPEntities _context;

        public UnitOfWork(IUserRepository userRepository, AHPEntities context)
        {
            this.UserRepository = userRepository;
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
