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
    class CriterionRepository : ICriterionRepository
    {
        private AHPEntities _context;
        private IMapper _mapper;
        public CriterionRepository(AHPEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICriterionModel Add(ICriterionModel criterion)
        {
            _context.Criteria.Add(_mapper.Map<ICriterionModel, Criterion>(criterion));
            return criterion;
        }

        public List<ICriterionModel> AddRange(List<ICriterionModel> criteria)
        {
            _context.Criteria.AddRange(_mapper.Map<List<ICriterionModel>, List<Criterion>>(criteria));
            return criteria;
        }

        public bool Delete(ICriterionModel criterion)
        {
            _context.Criteria.Remove(_mapper.Map<ICriterionModel, Criterion>(criterion));
            return true;
        }

        public async Task<ICriterionModel> GetByIDAsync(params Guid[] idValues)
        {
            var criterion = await _context.Criteria.FindAsync(idValues[0]);
            return _mapper.Map<Criterion, ICriterionModel>(criterion);
        }


        public async Task<ICriterionModel> UpdateAsync(ICriterionModel criterion)
        {
            var _criterion = await _context.Users.FindAsync(criterion.CriteriaID);
            _context.Entry(_criterion).CurrentValues.SetValues(_mapper.Map<ICriterionModel, Criterion>(criterion));
            return criterion;
        }

        public async Task<List<ICriterionModel>> GetPageByChoiceID(Guid choiceID, int pageNumber, int pageSize = 5)
        {
            var criteria = await _context.Criteria.Where(c => c.ChoiceID == choiceID).OrderBy(x => x.DateCreated).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map<List<Criterion>, List<ICriterionModel>>(criteria);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
