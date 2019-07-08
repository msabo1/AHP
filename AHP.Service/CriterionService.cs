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
        ICriterionRepository _critRepo;
        IAlternativeRepository _altRepo;

        public CriterionService(
            ICriterionService critRepo,
            IAlternativeService altRepo,
            ICriterionRepository critRepo,
            IAlternativeComparisonService altCompRepo,
            ICriteriaComparisonService critCompRepo,
            IUnitOfWorkFactory unitFactory)
        {
            _critRepo = critService;
            _altRepo = altService;
            _critRepo = critRepo;
            _altCompService = altCompService;
            _critCompRepo = critCompService;
            _unitFactory = unitFactory;
        }
        public async Task<ICriterionModel> AddAsync(ICriterionModel criterion)
        {
            
            criterion.CriteriaID = Guid.NewGuid();
            criterion.DateCreated = DateTime.Now;
            criterion.DateUpdated = DateTime.Now;
            var allCriteria = await _critRepo.GetAllAsync(criterion.ChoiceID);
            var allAlternatives = await _altRepo.GetAllAsync(criterion.ChoiceID);
            List<IAlternativeComparisonModel> acs = new List<IAlternativeComparisonModel>();
            List<ICriteriaComparisonModel> ccs = new List<ICriteriaComparisonModel>();
            IAlternativeComparisonModel altComp = new AlternativeComparisonModel();
            ICriteriaComparisonModel critComp = new CriteriaComparisonModel();
            foreach (var criteria in allCriteria)
            {
                critComp.CriteriaID1 = criterion.CriteriaID;
                critComp.CriteriaID2 = criteria.CriteriaID;
                critComp.CriteriaRatio = 1;

                ccs.Add(critComp);
            }
            for (int i = 0; i < allAlternatives.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    altComp.AlternativeID1 = allAlternatives[i].AlternativeID;
                    altComp.AlternativeID2 = allAlternatives[j].AlternativeID;
                    altComp.CriteriaID = criterion.CriteriaID;
                    altComp.AlternativeRatio = 1;
                }
                acs.Add(altComp);
            }
            using (var uof = _unitFactory.Create())
            {
                await _critCompRepo.AddAsync(ccs);
                await _altCompService.AddAsync(acs);
                criterion = _critRepo.Add(criterion);
                await _critRepo.SaveAsync();
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
