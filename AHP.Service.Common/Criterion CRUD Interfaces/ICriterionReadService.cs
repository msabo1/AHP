using AHP.Model.Common;
using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface ICriterionUpdateService
    {
        Task<ICriterionUpdateService> Check(ICriterionUpdateService criterion);
    }
}