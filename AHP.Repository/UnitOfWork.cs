using AHP.DAL;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AHP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AHPEntities _context;
        public TransactionScope transactionScope { get; }

        public UnitOfWork (AHPEntities context)
        {
            _context = context;
            transactionScope = new TransactionScope();
        }

        public async Task<int> SaveAsync()
        {
            int result = await _context.SaveChangesAsync();
            transactionScope.Complete();
            return result;
        }

        public void Commit()
        {
            transactionScope.Complete();
        }

        public void Dispose()
        {
        }
    }
}
