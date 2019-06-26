using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface IUserLogin
    {
        Task<bool> Check(string username, string password);
    }
}