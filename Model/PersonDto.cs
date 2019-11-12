using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NV2.Model
{
    public class PersonDto
    {
        public int BusinessEntityId { get; set; }
        public string PersonType { get; set; }
        public bool NameStyle { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
