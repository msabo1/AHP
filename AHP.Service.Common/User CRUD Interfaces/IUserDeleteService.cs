using AHP.Model.Common;

namespace AHP.Service.Common
{
    public interface IUserDeleteService
    {
        bool Delete(IUserModel user);
    }
}