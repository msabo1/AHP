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
    class AlternativeRepository : IRepository<Alternative>
    {
        private AHPEntities _context;
        public AlternativeRepository(AHPEntities context)
        {
            _context = context;
        }
        public async Task<Alternative> AddAsync(Alternative alternative)
        {
            _context.Alternatives.Add(alternative);
            await _context.SaveChangesAsync();
            return alternative;
        }

        public async Task<int> DeleteAsync(Alternative alternative)
        {
            _context.Alternatives.Remove(alternative);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Alternative>> GetAllAsync()
        {
            var alternatives = await _context.Alternatives.ToListAsync();
            return alternatives;
        }

        public async Task<Alternative> GetByIDAsync(Guid id)
        {
            var alternative = await _context.Alternatives.Where(a => a.AlternativeID == id).FirstAsync();
            await _context.Entry(alternative).Collection(a => a.AlternativeComparisons).LoadAsync();
            await _context.Entry(alternative).Collection(a => a.AlternativeComparisons1).LoadAsync();
            return alternative;
        }

        public async Task<Alternative> UpdateAsync(Alternative oldAlternative, Alternative newAlternative)
        {
            var alternative = await _context.Alternatives.Where(a => a == oldAlternative).FirstAsync();
            _context.Entry(alternative).CurrentValues.SetValues(newAlternative);
            await _context.SaveChangesAsync();
            return newAlternative;
        }
    }
}
