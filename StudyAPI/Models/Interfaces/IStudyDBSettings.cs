namespace StudyAPI.Models.Interfaces
{
    public interface IStudyDBSettings
    {
        string CommentsCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string PostsCollection { get; set; }
        string UserCollection { get; set; }
    }
}