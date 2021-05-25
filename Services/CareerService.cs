using MongoDB.Driver;
using PortfolioApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApi.Services
{
    public class CareerService
    {
        private readonly IMongoCollection<Models.Career> _career;

        public CareerService(Models.IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _career = database.GetCollection<Career>("Career");
        }

        public Career Create(Career career)
        {
            _career.InsertOne(career);
            return career;
        }

        public List<Career> Read() =>
            _career.Find(sub => true).ToList();

        public Career Find(string id) =>
            _career.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(string id, Career career) =>
            _career.ReplaceOne(sub => sub.Id == career.Id, career);

        public void Delete(string id) =>
            _career.DeleteOne(sub => sub.Id == id);
    }
}

