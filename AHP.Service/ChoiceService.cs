using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    class ChoiceService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        IChoiceRepository _choiceRepository;

        public ChoiceService(IUnitOfWorkFactory unitOfWorkFactory, IChoiceRepository choiceRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _choiceRepository = choiceRepository;
        }
        public async Task<IChoiceModel> CreateAsync(IChoiceModel choice)
        {
            choice.ChoiceID = Guid.NewGuid();
            choice.DateCreated = DateTime.Now;
            choice.DateUpdated = DateTime.Now;
            using (var uow = _unitOfWorkFactory.Create())
            {
                _choiceRepository.Add(choice);
                await _choiceRepository.SaveAsync();
                uow.Commit();
            }
            return choice;
        }
        public async Task<bool> DeleteAsync(IChoiceModel choice)
        {
            bool b = true;
            using (var uow = _unitOfWorkFactory.Create())
            {
                b = await _choiceRepository.DeleteAsync(choice);
                await _choiceRepository.SaveAsync();
                uow.Commit();
            }
            return b;
        }
        public async Task<List<IChoiceModel>> GetAsync(Guid userId, int page)
        {
            var choices = await _choiceRepository.GetChoicesByUserIDAsync(userId, page);
            return choices;
        }
        public async Task<IChoiceModel> UpdateAsync(IChoiceModel choice)
        {
            IChoiceModel updated;
            var _baseChoice = await _choiceRepository.GetByIDAsync(choice.ChoiceID);
            if (choice.ChoiceName != null) _baseChoice.ChoiceName = choice.ChoiceName;
            _baseChoice.DateUpdated = DateTime.Now;

            using (var uow = _unitOfWorkFactory.Create())
            {
                updated = await _choiceRepository.UpdateAsync(_baseChoice);
                await _choiceRepository.SaveAsync();
                uow.Commit();
            }
            return updated;
        }

    }
}
