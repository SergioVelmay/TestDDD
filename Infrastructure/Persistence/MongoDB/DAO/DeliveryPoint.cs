using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Infrastructure.Persistence.MongoDB.Helpers;
using Infrastructure.Shared.Exceptions;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.MongoDB.DAO
{
    public class DeliveryPoint : IMongoDAO<string>
    {
        public DeliveryPoint()
        {
            TableName = DbCollectionCatalog.DeliveryPoint;
        }

        [BsonElement("TableName")]
        public string TableName { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("CompanyId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CompanyId { get; set; }
        [BsonElement("Type")]
        public int? Type { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("CP")]
        public string CP { get; set; }

        [BsonIgnore]
        public Company Company { get; set; }

        public string GetId()
        {
            return Id;
        }

        public string GetTableName()
        {
            return TableName;
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


    }
}
