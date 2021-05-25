using System;
using MongoDB.Driver;
using PortfolioApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApi.Services
{
    public class IntroductionService
    {
        private readonly IMongoCollection<Models.Introduction> _introduction;

        public IntroductionService(Models.IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _introduction = database.GetCollection<Introduction>("Introduction");
        }

        public Introduction Create(Introduction introduction)
        {
            _introduction.InsertOne(introduction);
            return introduction;
        }

        public List<Introduction> Read() =>
            _introduction.Find(sub => true).ToList();

        public Introduction Find(string id) =>
            _introduction.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(string id, Introduction introduction) =>
            _introduction.ReplaceOne(sub => sub.Id == introduction.Id, introduction);

        public void Delete(string id) =>
            _introduction.DeleteOne(sub => sub.Id == id);
    }
}
