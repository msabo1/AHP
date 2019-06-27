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

        public async Task<List<IChoiceModel>> GetAsync(Guid userId, int page)
        {
            
            
                var choices = await _choiceRepository.GetChoicesByUserIDAsync(userId, page );

            return choices;
        }
    }
}
