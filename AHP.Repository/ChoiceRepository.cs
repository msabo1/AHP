using AHP.DAL;
using AHP.Model.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Repository.Common;

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
        public IChoiceModel Add(IChoiceModel choice)
        {

            _context.Choices.Add(_mapper.Map<IChoiceModel, Choice>(choice));
            return choice;
        }

        public async Task<IChoiceModel> GetByIDAsync(params Guid[] idValues)
        {
            var choice = await _context.Choices.FindAsync(idValues[0]);
            return _mapper.Map<Choice, IChoiceModel>(choice);
        }

        public async Task<IChoiceModel> UpdateAsync(IChoiceModel choice)
        {
            var _choice = await _context.Choices.FindAsync(choice.ChoiceID);
            _context.Entry(_choice).CurrentValues.SetValues(_mapper.Map<IChoiceModel, Choice>(choice));
            return choice;
        }

        public bool Delete(IChoiceModel choice)
        {
            _context.Choices.Remove(_mapper.Map<IChoiceModel, Choice>(choice));
            return true;
        }


        public async Task<List<IChoiceModel>> GetChoicesByUserID(Guid userID, int PageSize, int PageNumber)
        {
            var choices = await _context.Choices.Where(c => c.UserID == userID).OrderBy(x => x.DateCreated).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            return _mapper.Map<List<Choice>, List<IChoiceModel>>(choices);
        }

        public List<IChoiceModel> AddRange(List<IChoiceModel> choices)
        {
            var _choices = _mapper.Map<List<IChoiceModel>, List<Choice>>(choices);
            _context.Choices.AddRange(_choices);
            return choices;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
