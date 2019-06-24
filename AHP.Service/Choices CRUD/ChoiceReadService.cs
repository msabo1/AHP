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

namespace AHP.Service
{
    public class ChoiceReadService
    {
        IUnitOfWork _unitOfWork;

        public ChoiceReadService()
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IChoiceModel> Check(string choiceName)
        {
            IUserModel choice = await _unitOfWork.ChoiceRepository.GetByChoiceNameAsync(choiceName);

            if (choice != null)
            {
                if(choice.ChoiceName == choiceName)
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
