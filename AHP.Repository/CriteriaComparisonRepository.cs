using AHP.DAL;
using AHP.Model.Common;
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

        public ICriteriaComparisonModel Add(ICriteriaComparisonModel cc)
        {

            _context.CriteriaComparisons.Add(_mapper.Map<ICriteriaComparisonModel, CriteriaComparison>(cc));
            return cc;
        }

        public async Task<ICriteriaComparisonModel> GetByIDAsync(params Guid[] idValues)
        {
            var cc = await _context.CriteriaComparisons.FindAsync(idValues[0], idValues[1]);
            return _mapper.Map<CriteriaComparison, ICriteriaComparisonModel>(cc);
        }

        public async Task<ICriteriaComparisonModel> UpdateAsync(ICriteriaComparisonModel cc)
        {
            var _cc = await _context.CriteriaComparisons.FindAsync(cc.CriteriaID1,cc.CriteriaID2);
            _context.Entry(_cc).CurrentValues.SetValues(_mapper.Map<ICriteriaComparisonModel, CriteriaComparison>(cc));
            return cc;
        }

        public async Task<bool> DeleteAsync(ICriteriaComparisonModel cc)
        {
            var _cc = await _context.CriteriaComparisons.FindAsync(cc.CriteriaID1, cc.CriteriaID2);
            _context.CriteriaComparisons.Remove(_cc);
            return true;
        }

        public async Task<bool> DeleteByCriteriaIDAsync(Guid criteriaID)
        {
            var ccs = await _context.CriteriaComparisons.Where(cc => cc.CriteriaID1 == criteriaID || cc.CriteriaID2 == criteriaID).ToListAsync();
            _context.CriteriaComparisons.RemoveRange(ccs);
            return true;
        }

        public async Task<List<ICriteriaComparisonModel>> GetPageByCriterionIDAsync(Guid criteriaID,  int PageNumber, int PageSize = 5)
        {
            var ccs = await _context.CriteriaComparisons.Where(cc => cc.CriteriaID1 == criteriaID || cc.CriteriaID2 == criteriaID).OrderBy(x => x.DateCreated).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            return _mapper.Map<List<CriteriaComparison>, List<ICriteriaComparisonModel>>(ccs);
        }

        public async Task<List<ICriteriaComparisonModel>> GetByFirstCriterionIDAsync(Guid criteriaID)
        {
            var ccs = await _context.CriteriaComparisons.Where(cc => cc.CriteriaID1 == criteriaID).OrderBy(x => x.DateCreated).ToListAsync();
            return _mapper.Map<List<CriteriaComparison>, List<ICriteriaComparisonModel>>(ccs);
        }

        public List<ICriteriaComparisonModel> AddRange(List<ICriteriaComparisonModel> ccs)
        {
            var _ccs = _mapper.Map<List<ICriteriaComparisonModel>, List<CriteriaComparison>>(ccs);
            _context.CriteriaComparisons.AddRange(_ccs);
            return ccs;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
