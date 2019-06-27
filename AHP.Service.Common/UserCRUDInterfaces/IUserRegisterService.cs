using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IUserRegisterService
    {
        Task<IUserModel> CheckAsync(IUserModel user);
    }
}