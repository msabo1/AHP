using AHP.Model;
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
    class AlternativeService : IAlternativeService
    {
        IAlternativeRepository _altRepo;
        IUnitOfWorkFactory _unitOfWorkFactory;
        ICriterionRepository _critRepo;
        IAlternativeComparisonRepository _altCompRepo;
        public AlternativeService(
            IAlternativeRepository altRepo,
            IUnitOfWorkFactory unitOfWorkFactory,
            ICriterionRepository critRepo,
            IAlternativeComparisonRepository altCompRepo
            )
        {
            _altCompRepo = altCompRepo;
             _critRepo = critRepo;
            _altRepo = altRepo;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IAlternativeModel> AddAsync(IAlternativeModel alternative)
        {
            alternative.AlternativeID = Guid.NewGuid();
            alternative.DateCreated = DateTime.Now;
            alternative.DateUpdated = DateTime.Now;
            
            var allCriteria = await _critRepo.GetByChoiceIDAsync(alternative.ChoiceID);
            var allAlternatives = await _altRepo.GetByChoiceIDAsync(alternative.ChoiceID);
            List<IAlternativeComparisonModel> acs = new List<IAlternativeComparisonModel>();
            
            foreach (var criterion in allCriteria)
            {
                foreach (var alternative1 in allAlternatives)
                {
                    IAlternativeComparisonModel altComp = new AlternativeComparisonModel();
                    altComp.AlternativeID1 = alternative.AlternativeID;
                    altComp.AlternativeID2 = alternative1.AlternativeID;
                    altComp.CriteriaID = criterion.CriteriaID;
                    altComp.DateCreated = DateTime.Now;
                    altComp.DateUpdated = DateTime.Now;
                    altComp.AlternativeRatio = 1;
                    acs.Add(altComp);
                }
            }
            using (var uof = _unitOfWorkFactory.Create())
            {
                _altRepo.Add(alternative);
                 _altCompRepo.AddRange(acs);
                await _altRepo.SaveAsync();
                await _altCompRepo.SaveAsync();
                uof.Commit();
            }
               
            return alternative;
        }
        public async Task<bool> DeleteAsync(IAlternativeModel alternative)
        {
            
                var deleted = await _altRepo.DeleteAsync(alternative);
                await _altRepo.SaveAsync();
               
                return deleted;
            
        }
        public async Task<List<IAlternativeModel>> GetPageAsync(Guid id, int page = 1)
        {
            var alternatives = (await _altRepo.GetPageByChoiceIDAsync(id, page)).OrderByDescending(o => o.DateCreated).ToList();
            
            return alternatives;
        }
        public async Task<List<IAlternativeModel>> GetAllAsync(Guid id)
        {
            var alternatives = await _altRepo.GetByChoiceIDAsync(id);

            return alternatives;
        }
        public async Task<IAlternativeModel> UpdateAsync(IAlternativeModel alternative)
        {
            var _alternative = await _altRepo.GetByIDAsync(alternative.AlternativeID);
         
                _alternative.AlternativeName = alternative.AlternativeName;
                _alternative.DateUpdated = DateTime.Now;
                var updatedAlternative = await _altRepo.UpdateAsync(_alternative);
                await _altRepo.SaveAsync();
               
                return updatedAlternative;
           

        }
    }
}
