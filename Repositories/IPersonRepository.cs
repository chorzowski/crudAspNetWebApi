using System.Collections.Generic;
using System.Threading.Tasks;
using NV2.Model;

namespace NV2
{
    public interface IPersonRepository
    {
        void CreateOwner(Person person);
        void DeleteOwner(Person person);
        Task<IEnumerable<Person>> GetAllOwnersAsync();
        Task<Person> GetOwnerByIdAsync(int personId);
        void UpdateOwner(Person person);
    }
}