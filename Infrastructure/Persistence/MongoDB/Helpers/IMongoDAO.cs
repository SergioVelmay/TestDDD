using System;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.MongoDB.Helpers
{
    public interface IMongoDAO<TID> : IDAO<TID>
    {
        string GetTableName();
    }
}
