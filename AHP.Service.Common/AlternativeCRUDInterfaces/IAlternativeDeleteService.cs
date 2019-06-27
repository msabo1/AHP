using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IAlternativeDeleteService
    {
        Task<bool> Delete(IAlternativeModel alternative);
    }
}