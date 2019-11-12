using Microsoft.EntityFrameworkCore;
using NV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NV2
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AdventureWorks2016Context repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Person>> GetAllOwnersAsync()
        {
            return await FindAll()
               .OrderBy(ow => ow.FirstName)
               .ToListAsync();
        }

        public async Task<Person> GetOwnerByIdAsync(int personId)
        {
            return await FindByCondition(owner => owner.BusinessEntityId.Equals(personId))
                .FirstOrDefaultAsync();
        }

        //public async Task<Person> GetOwnerWithDetailsAsync(Guid personId)
        //{
        //    return await FindByCondition(owner => owner.BusinessEntityId.Equals(personId))
        //        .Include(ac => ac.Accounts)
        //        .FirstOrDefaultAsync();
        //}

        public void CreateOwner(Person person)
        {
            Create(person);
        }

        public void UpdateOwner(Person person)
        {
            Update(person);
        }

        public void DeleteOwner(Person person)
        {
            Delete(person);
        }
    }
}
