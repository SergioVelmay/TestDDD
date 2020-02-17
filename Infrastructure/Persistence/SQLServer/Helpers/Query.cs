using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Shared.Exceptions;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.SQLServer.Helpers
{
    public class Query<TDAO, TID> : BaseDb<TDAO, TID>, IQuery<TDAO, TID> where TDAO : class, IDAO<TID> 
    {
        public Query(IDbContext context) : base(context)
        {

        }

        public IEnumerable<TDAO> Find(Expression<Func<TDAO, bool>> func)
        {
            return GetDbSet().Where(func).ToList();
        }

        public async Task<IEnumerable<TDAO>> FindAsync(Expression<Func<TDAO, bool>> func)
        {
            return await GetDbSet().Where(func).ToListAsync();
        }

        public IEnumerable<TDAO> Find(string sqlRaw)
        {
            return GetDbSet().FromSqlRaw(sqlRaw).ToList();
        }

        public async Task<IEnumerable<TDAO>> FindAsync(string sqlRaw)
        {
            return await GetDbSet().FromSqlRaw(sqlRaw).ToListAsync();
        }

        public TDAO Get(Expression<Func<TDAO, bool>> func)
        {
            TDAO dao = GetDbSet().Where(func).FirstOrDefault();

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }

        public async Task<TDAO> GetAsync(Expression<Func<TDAO, bool>> func)
        {
            TDAO dao = await GetDbSet().Where(func).FirstOrDefaultAsync();

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }

        public TDAO Get(TID id)
        {
            TDAO dao = GetDbSet().Find(id);

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }

        public async Task<TDAO> GetAsync(TID id)
        {
            TDAO dao = await GetDbSet().FindAsync(id);

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }
    }
}
