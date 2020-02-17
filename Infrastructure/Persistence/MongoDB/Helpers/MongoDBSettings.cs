using System;
namespace Infrastructure.Persistence.MongoDB.Helpers
{
    public class MongoDBSettings
    {
        public string Database { get; set; }
        public string CollectionName { get; set; }
    }
}
