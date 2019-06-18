using System.Threading.Tasks;

namespace AHP.Service
{
    public interface IUserLogin
    {
        Task<string> Check(string username, string password);
    }
}