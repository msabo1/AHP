using System;
using System.Threading.Tasks;
using System.Transactions;

namespace AHP.Repository.Common
{
    public interface IUnitOfWork: IDisposable
    {
        object ChoiceRepository { get; set; }

        void Commit();

    }
}