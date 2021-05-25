using MongoDB.Driver;
using PortfolioApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApi.Services
{
    public class SkillService
    {
        private readonly IMongoCollection<Models.Skill> _skill;

        public SkillService(Models.IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _skill = database.GetCollection<Skill>("Skill");
        }

        public Skill Create(Skill skill)
        {
            _skill.InsertOne(skill);
            return skill;
        }

        public List<Skill> Read() =>
            _skill.Find(sub => true).ToList();

        public Skill Find(string id) =>
            _skill.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(string id, Skill skill) =>
            _skill.ReplaceOne(sub => sub.Id == skill.Id, skill);

        public void Delete(string id) =>
            _skill.DeleteOne(sub => sub.Id == id);
    }
}