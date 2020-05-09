using MongoDB.Bson;
using MongoDB.Driver;
using StudyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyAPI.Services
{
    public class Service
    {
        private readonly IMongoDatabase db;    

        public Service(IStudyDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            db = client.GetDatabase(settings.DatabaseName);
        }

        public async Task<IEnumerable<T>> Get<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> Get<T>(string Id, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", Id);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }    

        public async Task Insert<T>(string collectionName, T modelName)
        {
            var collection = db.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(modelName);
        }

        public async Task<bool> Update<T>(string Id, T modelName, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);

            var answer = await collection.ReplaceOneAsync(
            new BsonDocument("Id", Id),
            modelName,
            new ReplaceOptions { IsUpsert = true });

            return answer.IsAcknowledged && answer.UpsertedId > 0;
        }

        public async Task<bool> Delete<T>(string Id, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", Id);
            var answer = await collection.DeleteOneAsync(filter);

            return answer.IsAcknowledged && answer.DeletedCount > 0;
        }
    }
}