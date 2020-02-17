using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Infrastructure.Persistence.MongoDB.Helpers;
using Infrastructure.Shared.Exceptions;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.MongoDB.DAO
{
    public class Company : IMongoDAO<string>
    {
        public Company()
        {
            TableName = DbCollectionCatalog.Company;
        }

        [BsonElement("TableName")]
        public string TableName { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }

        public string GetId()
        {
            return Id;
        }

        public void SetNewId()
        {
            if (Id == null)
            {
                Id = ObjectId.GenerateNewId().ToString();
            }
            else
            {
                throw new PersistenceException("Cannot set a new id: Id is not null");
            }
        }

        public string GetTableName()
        {
            return TableName;
        }

    }
}
