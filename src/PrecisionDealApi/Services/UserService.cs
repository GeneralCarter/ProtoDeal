using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using PrecisionDealApi.Models;

namespace PrecisionDealApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersDB;
        public UserService(IConfigurationSettings settings) {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _usersDB = database.GetCollection<User>("Users");
        }

        public User Get(Guid id) =>
            _usersDB.Find<User>(u => u.Id.Equals(id)).FirstOrDefault();

        public User GetByUsername(string username) =>
            _usersDB.Find<User>(u => u.UserName == username).FirstOrDefault();

        public User Create(User user)
        {
            _usersDB.InsertOne(user);
            return user;
        }
    }
}