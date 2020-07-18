using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using PrecisionDealApi.Models;
using System.Linq;
using System.Collections.Generic;

namespace PrecisionDealApi.Services
{
    public class PropertyService
    {
        private readonly IMongoCollection<Property> _propertiesDB;

        private readonly UserContext _userContext;
        public PropertyService(IConfigurationSettings settings, UserContext userContext) {
            _userContext = userContext;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _propertiesDB = database.GetCollection<Property>("Properties");
        }

        public Property Get(string id) =>
            _propertiesDB.Find<Property>(p => p.Id.Equals(id)).FirstOrDefault();

        public List<Property> GetByUserId(string id) =>
            _propertiesDB.Find<Property>(p => p.UserId.Equals(id)).ToList();

        public Property Create(Property property)
        {
            property.UserId = _userContext.CurrentUser.Id;
            _propertiesDB.InsertOne(property);
            return property;
        }
    }
}