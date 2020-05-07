using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudyAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Uname { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Pword { get; set; }
    }
}