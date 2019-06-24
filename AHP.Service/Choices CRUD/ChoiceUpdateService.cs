using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Service.Common;

namespace AHP.Service
{
    public class ChoiceUpdateService
    {
        IUnitOfWork _unitOfWork;

        public ChoiceUpdateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IChoiceModel> Update(IChoiceModel choice)
        {
            return await _unitOfWork.ChoiceRepository.UpdateAsync(choice);
        }
    }
}
