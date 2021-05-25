using MongoDB.Driver;
using PortfolioApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApi.Services
{
    public class AboutMeService
    {
        private readonly IMongoCollection<Models.AboutMe> _aboutMe;

        public AboutMeService(Models.IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _aboutMe = database.GetCollection<AboutMe>("AboutMe");
        }

        public AboutMe Create(AboutMe aboutMe)
        {
            _aboutMe.InsertOne(aboutMe);
            return aboutMe;
        }

        public List<AboutMe> Read() =>
            _aboutMe.Find(sub => true).ToList();

        public AboutMe Find(string id) =>
            _aboutMe.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(string id, AboutMe aboutMe) =>
            _aboutMe.ReplaceOne(sub => sub.Id == aboutMe.Id, aboutMe);

        public void Delete(string id) =>
            _aboutMe.DeleteOne(sub => sub.Id == id);
    }
}