using System.Collections.Generic;
using StudyAPI.Models.Interfaces;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace StudyAPI.Services
{
    //Generic CRUD Service class which has methods/tasks that mostly work with all controllers due to its generic design
    public class Service
    {
        private readonly IMongoDatabase db;    

        public Service(IStudyDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            db = client.GetDatabase(settings.DatabaseName);
        }

        //Task gets a table out of the database
        public async Task<IEnumerable<T>> Get<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        //Task gets a table out in order of most recent (descending order) of the database
        public async Task<IEnumerable<T>> GetRecent<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            return await collection.Find(new BsonDocument()).Sort("{_id: -1}").ToListAsync(); //sorts out the posts by most recent (descending order) and returns them to a list with a set limit
        }

        //Task gets all comment records matching post id out of the database
        public async Task<IEnumerable<T>> GetCommentsByPost<T>(string postid, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("PostId", postid);

            return await collection.Find(filter).ToListAsync();
        }

        //Task gets all comment records matching user id out of the database
        public async Task<IEnumerable<T>> GetCommentsByUser<T>(string userid, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("UserId", userid);

            return await collection.Find(filter).Sort("{_id: -1}").ToListAsync();
        }

        //Task gets all post records matching user id out of the database
        public async Task<IEnumerable<T>> GetPostsByUser<T>(string userid, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("UserId", userid);

            return await collection.Find(filter).Sort("{_id: -1}").ToListAsync();
        }

        //Task gets a record from a table out of the database
        public async Task<T> Get<T>(string Id, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", Id);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        //Task gets the user table uname and pword fields for LOGIN USE from out of the database
        public async Task<T> GetLogin<T>(string uname, string pword, string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Uname", uname) & Builders<T>.Filter.Eq("Pword", pword);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        //Task inserts a new record into a table in the database
        public async Task Insert<T>(string collectionName, T modelName)
        {
            var collection = db.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(modelName);
        }

        //Task updates an existing record in a table in the database
        public async Task<bool> Update<T>(string Id, T modelName, string collectionName)
        {
            var itemId = new ObjectId(Id);
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", itemId);
            var answer = await collection.ReplaceOneAsync(
            filter,
            modelName,
            new ReplaceOptions { IsUpsert = true }
            );

            return answer.IsAcknowledged && answer.UpsertedId > 0;
        }

        //Task deletes a record by id in a table in the database
        public async Task<bool> Delete<T>(string Id, string collectionName)
        {
            var itemId = new ObjectId(Id);
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", itemId);

            if (itemId == null)
                return false;

            var answer = await collection.DeleteOneAsync(filter);

            return answer.IsAcknowledged && answer.DeletedCount > 0;
        }
    }
}