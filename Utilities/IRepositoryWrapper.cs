using System.Threading.Tasks;

namespace NV2
{
    public interface IRepositoryWrapper
    {
        PersonRepository Owner { get; }

        Task SaveAsync();
    }
}