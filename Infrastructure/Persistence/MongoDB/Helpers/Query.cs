using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.MongoDB.Helpers
{
    public class Query<TDAO,TID> : BaseDb<TDAO, TID>, IQuery<TDAO, TID> where TDAO : class, IMongoDAO<TID>, new()
    {
        public Query(IDbContext context) : base(context)
        {
        }

        private Expression<Func<TDAO, bool>> GetExpression(Expression<Func<TDAO, bool>> func)
        {
            var prefix = func.Compile();
            Expression<Func<TDAO, bool>> expr = item => prefix(item) && item.GetTableName() == GetTableName();

            //return expr;
            return func;
        }

        public IEnumerable<TDAO> Find(FilterDefinition<TDAO> filter)
        {
            return _context.GetMongoCollection().Find(GetBaseFilter().Append(filter)).ToList();
        }

        public async Task<IEnumerable<TDAO>> FindAsync(FilterDefinition<TDAO> filter)
        {
            return await _context.GetMongoCollection().Find(GetBaseFilter().Append(filter)).ToListAsync();
        }

        public IEnumerable<TDAO> Find(Expression<Func<TDAO, bool>> func)
        {
            return _context.GetMongoCollection().Find(GetExpression(func)).ToList();
        }

        public async Task<IEnumerable<TDAO>> FindAsync(Expression<Func<TDAO, bool>> func)
        {
            var x = _context.GetMongoCollection();
            //var y = GetExpression(func);
            var z = await x.Find(func).ToListAsync();
            //var z = await x.Find(y).ToListAsync();
            return z;
        }

        public async Task<IEnumerable<TDAO>> FindAllAsync()
        {
            var x = _context.GetMongoCollection();
            var y = await x.Find(x => true).ToListAsync();
            return y;
        }

        public TDAO Get(FilterDefinition<TDAO> filter)
        {
            return _context.GetMongoCollection().Find(GetBaseFilter().Append(filter)).FirstOrDefault();
        }

        public async Task<TDAO> GetAsync(FilterDefinition<TDAO> filter)
        {
            return await _context.GetMongoCollection().Find(GetBaseFilter().Append(filter)).FirstOrDefaultAsync();
        }

        public TDAO Get(Expression<Func<TDAO, bool>> func)
        {
            return _context.GetMongoCollection().Find(GetExpression(func)).FirstOrDefault();
        }

        public async Task<TDAO> GetAsync(Expression<Func<TDAO, bool>> func)
        {
            return await _context.GetMongoCollection().Find(GetExpression(func)).FirstOrDefaultAsync();
        }

        public TDAO Get(TID id)
        {
            return _context.GetMongoCollection().Find(GetBaseFilter().Append(GetIdFilter(id))).FirstOrDefault();
        }

        public async Task<TDAO> GetAsync(TID id)
        {
            return await _context.GetMongoCollection().Find(GetBaseFilter().Append(GetIdFilter(id))).FirstOrDefaultAsync();
        }
    }
}
