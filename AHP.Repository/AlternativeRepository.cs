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
    public class AlternativeRepository : IAlternativeRepository
    {
        private AHPEntities _context;
        IMapper _mapper;
        public AlternativeRepository(AHPEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AlternativeModel> AddAsync(AlternativeModel alternative)
        {
            _context.Alternatives.Add(_mapper.Map<AlternativeModel, Alternative>(alternative));
            await _context.SaveChangesAsync();
            return alternative;
        }

        public async Task<int> DeleteAsync(AlternativeModel alternative)
        {
            _context.Alternatives.Remove(_mapper.Map<AlternativeModel, Alternative>(alternative));
            return await _context.SaveChangesAsync();
        }

        public async Task<List<AlternativeModel>> GetAllAsync()
        {
            var alternatives = await _context.Alternatives.ToListAsync();
            return _mapper.Map<List<Alternative>, List<AlternativeModel>>(alternatives);
        }

        public async Task<AlternativeModel> GetByIDAsync(Guid id)
        {
            var alternative = await _context.Alternatives.Where(a => a.AlternativeID == id).FirstAsync();
            await _context.Entry(alternative).Collection(a => a.AlternativeComparisons).LoadAsync();
            await _context.Entry(alternative).Collection(a => a.AlternativeComparisons1).LoadAsync();
            return _mapper.Map<Alternative, AlternativeModel>(alternative);
        }

        public async Task<AlternativeModel> UpdateAsync(AlternativeModel oldAlternative, AlternativeModel newAlternative)
        {
            var _oldAlternative = _mapper.Map<AlternativeModel, Alternative>(oldAlternative);
            var alternative = await _context.Alternatives.Where(a => a == _oldAlternative).FirstAsync();
            _context.Entry(alternative).CurrentValues.SetValues(newAlternative);
            await _context.SaveChangesAsync();
            return _mapper.Map<Alternative, AlternativeModel>(alternative);
        }
    }
}
