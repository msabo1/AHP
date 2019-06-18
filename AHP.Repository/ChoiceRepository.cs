using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.DAL;
using AHP.Model;
using AHP.Repository.Common;
using AutoMapper;

namespace AHP.Repository
{
    public class ChoiceRepository : IChoiceRepository
    {

        private AHPEntities _context;
        IMapper _mapper;

        public ChoiceRepository(AHPEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChoiceModel> AddAsync(ChoiceModel choice)
        {
            _context.Choices.Add(_mapper.Map<ChoiceModel, Choice>(choice));
            await _context.SaveChangesAsync();
            return choice;
        }

        public async Task<List<ChoiceModel>> AddRangeAsync(List<ChoiceModel> choices)
        {
            _context.Choices.AddRange(_mapper.Map<List<ChoiceModel>, List<Choice>>(choices));
            await _context.SaveChangesAsync();
            return choices;
        }

        public async Task<int> DeleteAsync(ChoiceModel choice)
        {
            _context.Choices.Remove(_mapper.Map<ChoiceModel, Choice>(choice));
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ChoiceModel>> GetAllAsync()
        {
            var choices = await _context.Choices.ToListAsync();
            return _mapper.Map<List<Choice>, List<ChoiceModel>>(choices);
        }

        public async Task<ChoiceModel> GetByIDAsync(Guid id)
        {
            var choice = await _context.Choices.Where(c => c.ChoiceID == id).FirstAsync();
            await _context.Entry(choice).Collection(c => c.Criteria).LoadAsync();
            await _context.Entry(choice).Collection(a => a.Alternatives).LoadAsync();
            return _mapper.Map<Choice, ChoiceModel>(choice);
        }

        public async Task<ChoiceModel> UpdateAsync(ChoiceModel oldChoice, ChoiceModel newChoice)
        {
            var _oldChoice = _mapper.Map<ChoiceModel, Choice>(oldChoice);
            var choice = await _context.Choices.Where(c => c == _oldChoice).FirstAsync();
            _context.Entry(choice).CurrentValues.SetValues(newChoice);
            await _context.SaveChangesAsync();
            return _mapper.Map<Choice, ChoiceModel>(choice);
        }
    }
}