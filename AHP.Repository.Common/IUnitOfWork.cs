using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; set; }

        Task<int> SaveAsync();
    }
}