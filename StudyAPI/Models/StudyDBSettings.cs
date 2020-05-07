namespace StudyAPI.Models
{
    public class StudyDBSettings : IStudyDBSettings
    {
        public string UserCollection { get; set; }
        public string PostsCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}