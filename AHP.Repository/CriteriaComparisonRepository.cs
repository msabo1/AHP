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
    public class CriteriaComparisonRepository : ICriteriaComparisonRepository
    {
        private AHPEntities _context;
        IMapper _mapper;
        public CriteriaComparisonRepository(AHPEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CriteriaComparisonModel> AddAsync(CriteriaComparisonModel criteriaComparison)
        {

            _context.CriteriaComparisons.Add(_mapper.Map<CriteriaComparisonModel, CriteriaComparison>(criteriaComparison));
            await _context.SaveChangesAsync();
            return criteriaComparison;
        }

        public async Task<List<CriteriaComparisonModel>> GetAllAsync()
        {
            var criteriaComparisons = await _context.CriteriaComparisons.ToListAsync();
            return _mapper.Map<List<CriteriaComparison>, List<CriteriaComparisonModel>>(criteriaComparisons);
        }

        public async Task<CriteriaComparisonModel> GetByIDsAsync(Guid id1, Guid id2)
        {
            var criteriaComparison = await _context.CriteriaComparisons.Where(cc => cc.CriteriaID1 == id1 && cc.CriteriaID2 == id2).FirstAsync();
            return _mapper.Map<CriteriaComparison, CriteriaComparisonModel>(criteriaComparison);
        }

        public async Task<CriteriaComparisonModel> UpdateAsync(CriteriaComparisonModel oldComparison, CriteriaComparisonModel newComparison)
        {
            var _oldComparison = _mapper.Map<CriteriaComparisonModel, CriteriaComparison>(oldComparison);
            var comparison = await _context.CriteriaComparisons.Where(cc => cc == _oldComparison).FirstAsync();
            _context.Entry(comparison).CurrentValues.SetValues(newComparison);
            await _context.SaveChangesAsync();
            return _mapper.Map<CriteriaComparison, CriteriaComparisonModel>(comparison);
        }

        public async Task<int> DeleteAsync(CriteriaComparisonModel criteriaComparison)
        {
            _context.CriteriaComparisons.Remove(_mapper.Map<CriteriaComparisonModel, CriteriaComparison>(criteriaComparison));
            return await _context.SaveChangesAsync();
        }
    }
}

