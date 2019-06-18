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
    public class CriterionRepository : ICriterionRepository
    {
        private AHPEntities _context;
        IMapper _mapper;

        public CriterionRepository(AHPEntities context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CriterionModel> AddAsync(CriterionModel criterion)
        {
            _context.Criteria.Add(_mapper.Map<CriterionModel, Criterion>(criterion));
            await _context.SaveChangesAsync();
            return criterion;
        }

        public async Task<int> DeleteAsync(CriterionModel criterion)
        {
            _context.Criteria.Remove(_mapper.Map<CriterionModel, Criterion>(criterion));
            return await _context.SaveChangesAsync();
        }

        public async Task<List<CriterionModel>> GetAllAsync()
        {
            var criteria = await _context.Criteria.ToListAsync();
            return _mapper.Map<List<Criterion>, List<CriterionModel>>(criteria);
        }

        public async Task<CriterionModel> GetByIDAsync(Guid id)
        {
            var criterion = await _context.Criteria.Where(c => c.CriteriaID == id).FirstAsync();
            await _context.Entry(criterion).Collection(c => c.CriteriaComparisons).LoadAsync();
            await _context.Entry(criterion).Collection(c => c.CriteriaComparisons1).LoadAsync();
            return _mapper.Map<Criterion, CriterionModel>(criterion);
        }

        public async Task<CriterionModel> UpdateAsync(CriterionModel oldCriterion, CriterionModel newCriterion)
        {
            var _oldCriterion = _mapper.Map<CriterionModel, Criterion>(oldCriterion);
            var criterion = await _context.Criteria.Where(c => c == _oldCriterion).FirstAsync();
            _context.Entry(criterion).CurrentValues.SetValues(newCriterion);
            await _context.SaveChangesAsync();
            return _mapper.Map<Criterion, CriterionModel>(criterion);
        }
    }
}
