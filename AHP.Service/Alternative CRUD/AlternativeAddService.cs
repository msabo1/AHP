using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.Alternative_CRUD
{
    class AlternativeAddService
    {
        IUnitOfWork _unitOfWork;

        public AlternativeAddService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         public void Add(IAlternativeModel alternativa)
        {

        }
    }
}
