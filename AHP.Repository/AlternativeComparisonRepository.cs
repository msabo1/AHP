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
    class AlternativeComparisonRepository : IRepository<AlternativeComparison>
    {
        private AHPEntities _context;
        public AlternativeComparisonRepository(AHPEntities context)
        {
            _context = context;
        }
        public async Task<AlternativeComparison> AddAsync(AlternativeComparison altcomp)
        {
            _context.AlternativeComparisons.Add(altcomp);
            await _context.SaveChangesAsync();
            return altcomp;
        }

        public async Task<int> DeleteAsync(AlternativeComparison altcomp)
        {
            _context.AlternativeComparisons.Remove(altcomp);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<AlternativeComparison>> GetAllAsync()
        {
            var altcomp = await _context.AlternativeComparisons.ToListAsync();
            return altcomp;
        }

        public async Task<AlternativeComparison> GetByIDAsync(Guid idC, Guid idA1, Guid idA2)
        {
            var altcomp = await _context.AlternativeComparisons.Where(ac => ac.CriteriaID == idC && ac.AlternativeID1 == idA1 && ac.AlternativeID2 == idA2).FirstAsync();
            return altcomp;
        }

        public async Task<AlternativeComparison> UpdateAsync(AlternativeComparison oldAltcomp, AlternativeComparison newAltcomp)
        {
            var altcomp = await _context.AlternativeComparisons.Where(a => a == oldAltcomp).FirstAsync();
            _context.Entry(altcomp).CurrentValues.SetValues(newAltcomp);
            await _context.SaveChangesAsync();
            return newAltcomp;
        }
    }
}
