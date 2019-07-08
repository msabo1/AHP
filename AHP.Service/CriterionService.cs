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
    class CriterionService : ICriterionService
    {

        ICriterionRepository _critRepo;
        IAlternativeComparisonRepository _altCompRepo;
        ICriteriaComparisonRepository _critCompRepo;
        IUnitOfWorkFactory _unitFactory;
        IAlternativeRepository _altRepo;

        public CriterionService(
            ICriterionRepository critRepo,
            IAlternativeRepository altRepo,
            IAlternativeComparisonRepository altCompRepo,
            ICriteriaComparisonRepository critCompRepo,
            IUnitOfWorkFactory unitFactory)
        {
            _critRepo = critRepo;
            _altRepo = altRepo;
            _altCompRepo = altCompRepo;
            _critCompRepo = critCompRepo;
            _unitFactory = unitFactory;
        }
        public async Task<ICriterionModel> AddAsync(ICriterionModel criterion)
        {
            
            criterion.CriteriaID = Guid.NewGuid();
            criterion.DateCreated = DateTime.Now;
            criterion.DateUpdated = DateTime.Now;
            var allCriteria = await _critRepo.GetByChoiceIDAsync(criterion.ChoiceID);
            var allAlternatives = await _altRepo.GetByChoiceIDAsync(criterion.ChoiceID);
            List<IAlternativeComparisonModel> acs = new List<IAlternativeComparisonModel>();
            List<ICriteriaComparisonModel> ccs = new List<ICriteriaComparisonModel>();
            
          
            foreach (var criteria in allCriteria)
            {
                ICriteriaComparisonModel critComp = new CriteriaComparisonModel();
                critComp.CriteriaID1 = criterion.CriteriaID;
                critComp.CriteriaID2 = criteria.CriteriaID;
                critComp.CriteriaRatio = 1;
                critComp.DateCreated = DateTime.Now;
                critComp.DateUpdated = DateTime.Now;
                ccs.Add(critComp);
            }
            for (int i = 0; i < allAlternatives.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    IAlternativeComparisonModel altComp = new AlternativeComparisonModel();
                    altComp.AlternativeID1 = allAlternatives[i].AlternativeID;
                    altComp.AlternativeID2 = allAlternatives[j].AlternativeID;
                    altComp.CriteriaID = criterion.CriteriaID;
                    altComp.DateCreated = DateTime.Now;
                    altComp.DateUpdated = DateTime.Now;
                    altComp.AlternativeRatio = 1;
                    acs.Add(altComp);
                }
             
            }
            using (var uof = _unitFactory.Create())
            {
                criterion = _critRepo.Add(criterion);
                _critCompRepo.AddRange(ccs);
                _altCompRepo.AddRange(acs);
                await _critRepo.SaveAsync();
                await _critCompRepo.SaveAsync();
                await _altCompRepo.SaveAsync();
                uof.Commit();
            }
            
            return criterion;
        }
        public async Task<List<ICriterionModel>> GetPageAsync(Guid id, int page =1)
        {
            var criteria = await _critRepo.GetPageByChoiceIDAsync(id, page);
            return criteria;
        }
        public async Task<List<ICriterionModel>> GetAllAsync(Guid id)
        {
            var criteria = await _critRepo.GetByChoiceIDAsync(id);
            return criteria;
        }
        public async Task<bool> DeleteAsync(ICriterionModel criteria)
        {
            using (var uof = _unitFactory.Create())
            {
                var deleted = await _critRepo.DeleteAsync(criteria);
                await _critRepo.SaveAsync();
                uof.Commit();
                return deleted;
            }
        }
        public async Task<bool> UpdateAsync(ICriterionModel criteria)
        {
           
                var updated = await _critRepo.UpdateAsync(criteria);
                await _critRepo.SaveAsync();
              
                return true;

            
     
        }
    }
}
