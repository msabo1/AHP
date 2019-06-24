
using AHP.Model.Common;
using AHP.Repository.Common;
using AHP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AHP.Service
{

    public class ChoiceCreateService : IChoiceCreateService
    {
        IUnitOfWork _unitOfWork;

        public ChoiceCreateService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IChoiceModel> Check(IChoiceModel choice)
        {
            IChoiceModel _choice = await _unitOfWork.ChoiceRepository.GetByChoiceNameAsync(choice.ChoiceName);

            if (_choice != null)
            {
                return null;
            }
            else
            {
                choice.ChoiceID = Guid.NewGuid();
                //choice.UserID = Guid.NewGuid();
                choice.DateCreated = DateTime.Now;
                choice.DateUpdated = DateTime.Now;
                choice = _unitOfWork.ChoiceRepository.Add(choice);

                //Try catch nekakav ovdje za provjeru jel uspjela transakcija
                await _unitOfWork.SaveAsync();
                return choice;
            }
        }
    }
}
