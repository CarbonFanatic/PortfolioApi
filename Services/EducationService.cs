using MongoDB.Driver;
using PortfolioApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApi.Services
{
    public class EducationService
    {
        private readonly IMongoCollection<Education> _education;

        public EducationService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _education = database.GetCollection<Education>("Education");
        }

        public Education Create(Education education)
        {
            _education.InsertOne(education);
            return education;
        }

        public List<Education> Get() =>
            _education.Find(edu => true).ToList();

        public Education Find(string id) =>
            _education.Find(edu => edu.Id == id).SingleOrDefault();

          public void Update(string id, Education eduIn) =>
            _education.ReplaceOne(edu => edu.Id == id, eduIn);

        public void Delete(string id) =>
            _education.DeleteOne(edu => edu.Id == id);
    }
}

