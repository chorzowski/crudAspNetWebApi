using NV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NV2
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AdventureWorks2016Context _adventureWorks2016ContextContext;
        private PersonRepository _owner;
        // in this place i will put next repositories

        public PersonRepository Owner
        {
            get
            {
                if (_owner == null)
                {
                    _owner = new PersonRepository(_adventureWorks2016ContextContext);
                }

                return _owner;
            }
        }

        public RepositoryWrapper(AdventureWorks2016Context repositoryContext)
        {
            _adventureWorks2016ContextContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _adventureWorks2016ContextContext.SaveChangesAsync();
        }
    }
}

