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
    class AlternativeGetService : IAlternativeGetService
    {
        IAlternativeRepository _altRepo;
        IUnitOfWorkFactory _unitOfWorkFactory;
        public AlternativeGetService(
            IAlternativeRepository altRepo,
            IUnitOfWorkFactory unitOfWorkFactory
            )
        {
            _altRepo = altRepo;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IChoiceModel> GetAsync(IChoiceModel choice, int page = 1)
        {
            var comparisons = await _altRepo.GetAlternativesByChoiceIDAsync(choice.ChoiceID, page);

            foreach (IAlternativeModel comparison in comparisons)
            {
                choice.Alternatives.Add(comparison);
            }

            return choice;
        }

    }
}
