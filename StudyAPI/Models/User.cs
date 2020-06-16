using MongoDB.Bson;
using StudyAPI.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace StudyAPI.Models
{
    //Model class to get and set data for the table and database, an interface of the model is used to ensure we aren't directly taking it from the model itself
    public class User : IUser
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