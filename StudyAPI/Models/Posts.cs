using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudyAPI.Models
{
    public class Posts
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}