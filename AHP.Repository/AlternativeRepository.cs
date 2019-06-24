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
    class AlternativeRepository : IAlternativeRepository
    {
        private AHPEntities _context;
        IMapper _mapper;
        public AlternativeRepository(AHPEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IAlternativeModel Add(IAlternativeModel alternative)
        {

            _context.Alternatives.Add(_mapper.Map<IAlternativeModel, Alternative>(alternative));
            return alternative;
        }

        public async Task<IAlternativeModel> GetByIDAsync(params Guid[] idValues)
        {
            var alternative = await _context.Alternatives.FindAsync(idValues);
            return _mapper.Map<Alternative, IAlternativeModel>(alternative);
        }

        public async Task<IAlternativeModel> UpdateAsync(IAlternativeModel alternative)
        {
            var _user = await _context.Alternatives.FindAsync(alternative.AlternativeID);
            _context.Entry(_user).CurrentValues.SetValues(_mapper.Map<IAlternativeModel, Alternative>(alternative));
            return alternative;
        }

        public bool Delete(IAlternativeModel alternative)
        {
            _context.Alternatives.Remove(_mapper.Map<IAlternativeModel, Alternative>(alternative));
            return true;
        }


        public async Task<List<IAlternativeModel>> GetAlternativesByChoiceID(Guid choiceID, int PageSize, int PageNumber)
        {
            var alternatives = await _context.Alternatives.Where(c => c.ChoiceID == choiceID).OrderBy(x => x.DateCreated).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            return _mapper.Map<List<Alternative>, List<IAlternativeModel>>(alternatives);
        }

        public List<IAlternativeModel> AddRange(List<IAlternativeModel> alternatives)
        {
            var _alternatives = _mapper.Map<List<IAlternativeModel>, List<Alternative>>(alternatives);
            _context.Alternatives.AddRange(_alternatives);
            return alternatives;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
