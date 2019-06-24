using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    public class ChoiceDeleteService
    {
        IUnitOfWork _unitOfWork;

        public ChoiceDeleteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Delete (IChoiceModel choice)
        {
            return _unitOfWork.ChoiceRepository.Delete(choice);
        }
    }
}
