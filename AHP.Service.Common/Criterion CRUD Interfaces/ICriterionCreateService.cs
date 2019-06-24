using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface ICriterionUpdateService
    {
        Task<ICriterionUpdateService> Check(ICriterionUpdateService criterion);
    }
}