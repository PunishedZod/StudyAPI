﻿namespace StudyAPI.Models.Interfaces
{
    public interface IComments
    {
        string Comment { get; set; }
        string Id { get; set; }
        string PostId { get; set; }
        string UserId { get; set; }
        string Uname { get; set; }
    }
}