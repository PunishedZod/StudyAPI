using MongoDB.Bson;
using MongoDB.Driver;
using StudyAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyAPI.Services
{
    //Generic CRUD Service class which works with all controllers due to its generic design
    public class Service
    {
        private readonly IMongoDatabase db;    

        public Service(IStudyDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            db = client.GetDatabase(settings.DatabaseName);
        }

        //Task gets table out of the database
        public async Task<IEnumerable<T>> Get<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        //Task gets table out in order of most recent (descending order) of the database
        public async Task<IEnumerable<T>> GetRecent<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            return await collection.Find(new BsonDocument()).Sort("{_id: -1}").ToListAsync(); //sorts out the posts by most recent (descending order) and returns them to a list with a set limit
        }

        //Task gets table out in order of popularity out of the database
        public async Task<IEnumerable<T>> GetPopular<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            return await collection.Find(new BsonDocument()).Sort("{UpVote: -1}").Limit(30).ToListAsync(); //sorts out the posts by popularity in descending order and returns them to a list with a set limit
        }

        //Task gets specific data from a table out of the database
        public async Task<T> Get<T>(string Id, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", Id);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        //Task gets specific USER data for LOGIN USE from a table out of the database
        public async Task<T> GetLogin<T>(string uname, string pword, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Uname", uname) & Builders<T>.Filter.Eq("Pword", pword);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        //Task inserts new data into a table in the database
        public async Task Insert<T>(string collectionName, T modelName)
        {
            var collection = db.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(modelName);
        }

        //Task updates existing data in a table in the database
        public async Task<bool> Update<T>(string Id, T modelName, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);

            var answer = await collection.ReplaceOneAsync(
            new BsonDocument("Id", Id),
            modelName,
            new ReplaceOptions { IsUpsert = true });

            return answer.IsAcknowledged && answer.UpsertedId > 0;
        }

        //Task delets specific data in a table in the database
        public async Task<bool> Delete<T>(string Id, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", Id);
            var answer = await collection.DeleteOneAsync(filter);

            return answer.IsAcknowledged && answer.DeletedCount > 0;
        }
    }
}