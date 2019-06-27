using System.Threading.Tasks;
using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IUserUpdateService
    {
        Task<IUserModel> UpdateAsync(IUserModel user);
    }
}