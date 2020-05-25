﻿namespace StudyAPI.Models.Interfaces
{
    public interface IPosts
    {
        string Content { get; set; }
        int DownVote { get; set; }
        string Id { get; set; }
        string Title { get; set; }
        string Topic { get; set; }
        string Uname { get; set; }
        int UpVote { get; set; }
    }
}