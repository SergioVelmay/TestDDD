using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Infrastructure.Persistence.MongoDB.Contexts;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.MongoDB.Helpers
{
    public class Persistence<TDAO, TID> : BaseDb<TDAO, TID>, IPersistence<TDAO, TID> where TDAO : class, IMongoDAO<TID>, new()
    {
        public Persistence(IDbContext context) : base(context)
        {
            
        }

        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.BeginTransactionAsync();
        }

        public void Commit()
        {
            _context.Commit();
        }

        public async Task CommitAsync()
        {
            await _context.CommitAsync();
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public async Task RollbackAsync()
        {
            await _context.RollbackAsync();
        }

        public void Delete(TDAO item)
        {
            _context.GetMongoCollection().DeleteOne(GetBaseFilter().Append(GetIdFilter(item)));
        }

        public async Task DeleteAsync(TDAO item)
        {   
            await _context.GetMongoCollection().DeleteOneAsync(GetBaseFilter().Append(GetIdFilter(item)));
        }

        public void Delete(IEnumerable<TDAO> items)
        {
            _context.GetMongoCollection().DeleteMany(GetBaseFilter().Append(GetIdFilter(items)));
        }

        public async Task DeleteAsync(IEnumerable<TDAO> items)
        {
            await _context.GetMongoCollection().DeleteManyAsync(GetBaseFilter().Append(GetIdFilter(items)));
        }

        public void Delete(TID id)
        {
            _context.GetMongoCollection().FindOneAndDelete(GetBaseFilter().Append(GetIdFilter(id)));
        }

        public async Task DeleteAsync(TID id)
        {
            await _context.GetMongoCollection().FindOneAndDeleteAsync(GetBaseFilter().Append(GetIdFilter(id)));
        }

        public TDAO Insert(TDAO item)
        {
            _context.GetMongoCollection().InsertOne(item);
            return item;
        }

        public async Task<TDAO> InsertAsync(TDAO item)
        {
            await _context.GetMongoCollection().InsertOneAsync(item);
            return item;
        }

        public TDAO Update(TDAO item)
        {
            _context.GetMongoCollection().ReplaceOne(GetBaseFilter().Append(GetIdFilter(item)), item);
            return item;
        }

        public async Task<TDAO> UpdateAsync(TDAO item)
        {
            await _context.GetMongoCollection().ReplaceOneAsync(GetBaseFilter().Append(GetIdFilter(item)), item);
            return item;
        }
    }
}
