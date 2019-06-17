using AHP.DAL;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository
{
    class CriterionRepository : IRepository<Criterion>
    {
        private AHPEntities _context;

        public CriterionRepository(AHPEntities context) 
        {
            _context = context;
        }
        public async Task<Criterion> AddAsync(Criterion criterion)
        {
            _context.Criteria.Add(criterion);
            await _context.SaveChangesAsync();
            return criterion;
        }

        public async Task<int> DeleteAsync(Criterion criterion)
        {
            _context.Criteria.Remove(criterion);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Criterion>> GetAllAsync()
        {
            var criteria = await _context.Criteria.ToListAsync();
            return criteria;
        }

        public async Task<Criterion> GetByIDAsync(Guid id)
        {
            var criterion = await _context.Criteria.Where(c => c.CriteriaID == id).FirstAsync();
            await _context.Entry(criterion).Collection(c => c.CriteriaComparisons).LoadAsync();
            await _context.Entry(criterion).Collection(c => c.CriteriaComparisons1).LoadAsync();
            return criterion;
        }

        public async Task<Criterion> UpdateAsync(Criterion oldCriterion, Criterion newCriterion)
        {
            var criterion = await _context.Criteria.Where(c => c == oldCriterion).FirstAsync();
            _context.Entry(criterion).CurrentValues.SetValues(newCriterion);
            await _context.SaveChangesAsync();
            return newCriterion;
        }
    }
}
