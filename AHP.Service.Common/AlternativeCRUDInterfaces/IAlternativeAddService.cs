using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IAlternativeAddService
    {
        Task<IAlternativeModel> Add(IAlternativeModel alternative);
    }
}