using StartGraphQL.Contracts;
using StartGraphQL.Entities;
using StartGraphQL.Entities.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartGraphQL.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationContext _context;
        public OwnerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Owner> GetAll() => _context.Owners.ToList();
    }
}
