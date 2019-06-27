using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IAlternativeUpdateService
    {
        Task<IAlternativeModel> Update(IAlternativeModel alternative);
    }
}