using AHP.Model.Common;
using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface IUserDeleteService
    {
        Task<bool> Delete(IUserModel user);
    }
}