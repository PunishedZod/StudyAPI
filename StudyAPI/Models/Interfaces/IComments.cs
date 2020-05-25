namespace StudyAPI.Models.Interfaces
{
    public interface IComments
    {
        string Comment { get; set; }
        int DownVote { get; set; }
        string Id { get; set; }
        string PostId { get; set; }
        string Uname { get; set; }
        int UpVote { get; set; }
    }
}