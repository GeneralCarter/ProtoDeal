using System;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace PrecisionDealApi.Models
{
    public class Property
    {
        public Property() {}

        public string Id { get; set; }
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Price { get; set; }
        public double ClosingCost { get; set; }
        public double DownPayment { get; set; }

        public string UserId { get; set; }

    }
}