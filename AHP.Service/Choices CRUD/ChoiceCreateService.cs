
using AHP.Model.Common;
using AHP.Repository.Common;
using AHP.Service.Common;
using AHP.Service.Common.Choice_CRUD_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AHP.Service
{

    public class ChoiceCreateService : IChoiceCreateService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        IChoiceRepository _choiceRepository;

        public ChoiceCreateService(IUnitOfWorkFactory unitOfWorkFactory, IChoiceRepository choiceRepository)
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
    }
}
