﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StudyAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public string PostId { get; set; }
    }
}