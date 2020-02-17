using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.SQLServer.Contexts;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.SQLServer.Helpers
{
    public class BaseDb<TDAO, TID> where TDAO : class, IDAO<TID>
    {
        protected readonly GenericDbContext<TDAO> _context;
        protected IMapper _mapper;

        public BaseDb(IDbContext context)
        {
            _context = (GenericDbContext<TDAO>)context;
        }

        protected DbSet<TDAO> GetDbSet()
        {
            return _context.GetItems();
        }

        protected TID GetId(TDAO item)
        {
            return item.GetId();
        }
    }
}
