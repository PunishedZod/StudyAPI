using MongoDB.Bson.Serialization.Attributes;
using StudyAPI.Models.Interfaces;
using MongoDB.Bson;

namespace StudyAPI.Models
{
    //Model class to get and set data for the table and database, an interface of the model is used to ensure we aren't directly taking it from the model itself
    public class Comments : IComments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Uname { get; set; }
        public string Comment { get; set; }
        public string PostId { get; set; }
        public string UId { get; set; }
    }
}