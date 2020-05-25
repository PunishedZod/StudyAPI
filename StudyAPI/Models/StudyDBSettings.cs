using StudyAPI.Models.Interfaces;

namespace StudyAPI.Models
{
    //Model class to get and set data for the table and database, an interface of the model is used to ensure we aren't directly taking it from the model itself
    public class StudyDBSettings : IStudyDBSettings
    {
        public string UserCollection { get; set; }
        public string PostsCollection { get; set; }
        public string CommentsCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}