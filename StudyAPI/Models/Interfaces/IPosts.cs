namespace StudyAPI.Models.Interfaces
{
    public interface IPosts
    {
        string Content { get; set; }
        string[] DownVoteId { get; set; }
        string Id { get; set; }
        string Title { get; set; }
        string Topic { get; set; }
        string Uname { get; set; }
        string UId { get; set; }
        string[] UpVoteId { get; set; }
    }
}