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

        public ChoiceDeleteService(IUnitOfWorkFactory unitOfWorkFactory, IChoiceRepository choiceRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _choiceRepository = choiceRepository;
        }

        public async Task<bool> Delete(IChoiceModel choice)
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
