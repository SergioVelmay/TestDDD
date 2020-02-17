using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MongoDB.Driver;
using Infrastructure.Persistence.MongoDB.Contexts;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.MongoDB.Helpers
{
    public class BaseDb<TDAO, TID> where TDAO : class, IMongoDAO<TID>, new()
    {
        protected readonly GenericDbContext<TDAO> _context;
        protected readonly string _tableName;

        public BaseDb(IDbContext context)
        {
            _context = (GenericDbContext<TDAO>)context;
            _tableName = new TDAO().GetTableName();
        }

        protected FilterDefinition<TDAO> GetIdFilter(TID id)
        {
            return GetBaseFilter().Append(GetFilter().Eq("_id", id));
        }

        protected FilterDefinition<TDAO> GetIdFilter(TDAO item)
        {
            return GetBaseFilter().Append(GetFilter().Eq("_id", item.GetId()));
        }

        protected FilterDefinition<TDAO> GetIdFilter(IEnumerable<TDAO> items)
        {
            return GetBaseFilter().Append(GetFilter().In("_id", items.Select(item => item.GetId())));
        }

        protected string GetTableName()
        {
            return _tableName;
        }

        internal FilterDefinition<TDAO> GetBaseFilter()
        {
            return Builders<TDAO>.Filter.GetBaseFilter(_tableName);
        }

        internal FilterDefinitionBuilder<TDAO> GetFilter()
        {
            return Builders<TDAO>.Filter;
        }

        internal IMongoCollection<TDAO> GetMongoCollection()
        {
            return _context.GetMongoCollection();
        }
    }
}
