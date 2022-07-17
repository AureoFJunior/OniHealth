using System.Threading.Tasks;

namespace OniHealth.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}