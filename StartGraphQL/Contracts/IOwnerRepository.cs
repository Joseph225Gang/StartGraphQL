using StartGraphQL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartGraphQL.Contracts
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAll();
    }
}
