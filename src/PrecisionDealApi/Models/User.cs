using System;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PrecisionDealApi.Models
{
    public class User : IdentityUser<string>
    {
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public new string Id { get; set; }
    }
}