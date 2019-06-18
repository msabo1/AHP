using AHP.DAL;
using AHP.Model;
using AHP.Repository.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository
{
    public class AlternativeComparisonRepository : IAlternativeComparisonRepository
    {
        private AHPEntities _context;
        IMapper _mapper;
        public AlternativeComparisonRepository(AHPEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AlternativeComparisonModel> AddAsync(AlternativeComparisonModel altcomp)
        {
            _context.AlternativeComparisons.Add(_mapper.Map<AlternativeComparisonModel, AlternativeComparison>(altcomp));
            await _context.SaveChangesAsync();
            return altcomp;
        }

        public async Task<int> DeleteAsync(AlternativeComparisonModel altcomp)
        {
            _context.AlternativeComparisons.Remove(_mapper.Map<AlternativeComparisonModel, AlternativeComparison>(altcomp));
            return await _context.SaveChangesAsync();
        }

        public async Task<List<AlternativeComparisonModel>> GetAllAsync()
        {
            var altcomps = await _context.AlternativeComparisons.ToListAsync();
            return _mapper.Map<List<AlternativeComparison>, List<AlternativeComparisonModel>>(altcomps);
        }

        public async Task<AlternativeComparisonModel> GetByIDsAsync(Guid idC, Guid idA1, Guid idA2)
        {
            var altcomp = await _context.AlternativeComparisons.Where(ac => ac.CriteriaID == idC && ac.AlternativeID1 == idA1 && ac.AlternativeID2 == idA2).FirstAsync();
            return _mapper.Map<AlternativeComparison, AlternativeComparisonModel>(altcomp);
        }

        public async Task<AlternativeComparisonModel> UpdateAsync(AlternativeComparisonModel oldAltcomp, AlternativeComparisonModel newAltcomp)
        {
            var _oldAltcomp = _mapper.Map<AlternativeComparisonModel, AlternativeComparison>(oldAltcomp);
            var altcomp = await _context.AlternativeComparisons.Where(a => a == _oldAltcomp).FirstAsync();
            _context.Entry(altcomp).CurrentValues.SetValues(newAltcomp);
            await _context.SaveChangesAsync();
            return _mapper.Map<AlternativeComparison, AlternativeComparisonModel>(altcomp);
        }
    }
}
