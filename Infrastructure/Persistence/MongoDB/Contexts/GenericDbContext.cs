using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Infrastructure.Persistence.MongoDB.Helpers;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.MongoDB.Contexts
{
    public class GenericDbContext<TDAO> : IDbContext where TDAO : class
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _mongoClient;
        private readonly MongoDBSettings _settings;
        private IClientSessionHandle _session;

        public GenericDbContext(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            _mongoClient = mongoClient;
            _settings = settings.Value;
            _database = _mongoClient.GetDatabase(_settings.Database);
        }

        internal IMongoCollection<TDAO> GetMongoCollection()
        {
            var x = _database.GetCollection<TDAO>(_settings.CollectionName);
            return x;
        }

        public void BeginTransaction()
        {
            _session = _mongoClient.StartSession();
        }

        public async Task BeginTransactionAsync()
        {
            _session = await _mongoClient.StartSessionAsync();
        }

        public void Commit()
        {
            if (_session != null)
            {
                _session.CommitTransaction();
            }
            _session = null;
        }

        public async Task CommitAsync()
        {
            if (_session != null)
            {
                await _session.CommitTransactionAsync();
            }
            _session = null;
        }

        public void Rollback()
        {
            if (_session != null)
            {
                _session.AbortTransaction();
            }
            _session = null;
        }

        public async Task RollbackAsync()
        {
            if (_session != null)
            {
                await _session.AbortTransactionAsync();
            }
            _session = null;
        }
        
    }
}
