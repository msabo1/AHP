using AHP.Repository.Common;
using AHP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Model;
using AHP.Service.Common;
using AHP.Model.Common;
using AHP.Service.Common.Choice_CRUD_Interfaces;

namespace AHP.Service
{
    public class ChoiceReadService : IChoiceReadService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        IChoiceRepository _choiceRepository;

        public ChoiceReadService(IUnitOfWorkFactory unitOfWorkFactory, IChoiceRepository choiceRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _choiceRepository = choiceRepository;
        }

        public async Task<IChoiceModel> Check(Guid choiceID)
        {
            IChoiceModel choice;
            using (var uow = _unitOfWorkFactory.Create())
            {
                choice = await _choiceRepository.GetByIDAsync(choiceID);
                uow.Commit();
            }
            if (choice != null)
            {
                if(choice.ChoiceID == choiceID)
                {
                    return choice;
                }
                else
                {
                    choice = null;
                    return choice;
                }
            }
            else
            {
                return choice;
            }
        }
    }
}
