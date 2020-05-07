namespace StudyAPI.Models
{
    public interface IStudyDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string PostsCollection { get; set; }
        string UserCollection { get; set; }
    }
}