using AHP.Model.Common;
using AHP.Repository.Common;
using AHP.Service.Common.Choice_CRUD_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    public class ChoiceDeleteService : IChoiceDeleteService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        IChoiceRepository _choiceRepository;
        ICriterionRepository _criterionRepository;
        IAlternativeRepository _alternativeRepository;

        public ChoiceDeleteService(
            IUnitOfWorkFactory unitOfWorkFactory, 
            IChoiceRepository choiceRepository,
            ICriterionRepository criterionRepository,
            IAlternativeRepository alternativeRepository
            )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _choiceRepository = choiceRepository;
            _criterionRepository = criterionRepository;
            _alternativeRepository = alternativeRepository;
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
    }
}
