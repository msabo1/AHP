using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IAlternativeGetService
    {
        Task<IChoiceModel> Get(IChoiceModel choice, int page = 1);
    }
}