using AHP.Model.Common;

namespace AHP.Service
{
    interface IUserDeleteService
    {
        bool Delete(IUserModel user);
    }
}