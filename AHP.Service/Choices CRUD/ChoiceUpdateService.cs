using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Service.Common;
using AHP.Service.Common.Choice_CRUD_Interfaces;

namespace AHP.Service
{
    public class ChoiceUpdateService : IChoiceUpdateService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        IChoiceRepository _choiceRepository;

        public ChoiceUpdateService(IUnitOfWorkFactory unitOfWorkFactory, IChoiceRepository choiceRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _choiceRepository = choiceRepository;
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
