System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.DAL;
using AHP.Repository.Common;

namespace AHP.Repository
{
    class ChoiceRepository : IRepository<Choice>
    {

        private AHPEntities _context;

        public ChoiceRepository(AHPEntities context)
        {
            _context = context;
        }

        public async Task<Choice> AddAsync(Choice choice)
        {
            _context.Choices.Add(choice);
            await _context.SaveChangesAsync();
            return choice;
        }

        public async Task<List<Choice>> AddRangeAsync(List<Choice> choices)
        {
            _context.Choices.AddRange(choices);
            await _context.SaveChangesAsync();
            return choices;
        }

        public async Task<int> DeleteAsync(Choice choice)
        {
            _context.Choices.Remove(choice);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Choice>> GetAllAsync()
        {
            var choices = await _context.Choices.ToListAsync();
            return choices;
        }

        public async Task<Choice> GetByIDAsync(Guid id)
        {
            var choice = await _context.Choices.Where(u => u.ChoiceID == id).FirstAsync();
            await _context.Entry(choice).Collection(u => u.Criteria).LoadAsync();
            await _context.Entry(choice).Collection(u => u.Alternatives).LoadAsync();
            return choice;
        }

        public async Task<Choice> UpdateAsync(Choice oldChoice, Choice newChoice)
        {
            var choice = await _context.Choices.Where(u => u == oldChoice).FirstAsync();
            _context.Entry(choice).CurrentValues.SetValues(newChoice);
            await _context.SaveChangesAsync();
            return newChoice;
        }
    }
}