using SkopeiAPI.Repositories;
using System.Threading.Tasks;

namespace SkopeiAPI.UnitOfWorks
{
    public interface IUnitOfWork
    {
        UserRepository UserRepo { get; }
        ProductRepository ProductRepo { get; }
        Task SaveAsync();
    }
}
