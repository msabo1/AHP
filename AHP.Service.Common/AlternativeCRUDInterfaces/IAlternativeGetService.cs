using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IAlternativeGetService
    {
        Task<IAlternativeModel> Get(IAlternativeModel alternative);
    }
}