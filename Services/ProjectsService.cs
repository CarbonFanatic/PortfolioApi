using MongoDB.Driver;
using PortfolioApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApi.Services
{
    public class ProjectsService
    {

        private readonly IMongoCollection<Models.Projects> _projects;

        public ProjectsService(Models.IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _projects = database.GetCollection<Projects>("Projects");
        }

        public Projects Create(Projects projects)
        {
            _projects.InsertOne(projects);
            return projects;
        }

        public List<Projects> Read() =>
            _projects.Find(sub => true).ToList();

        public Projects Find(string id) =>
            _projects.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(string id, Projects projects) =>
            _projects.ReplaceOne(sub => sub.Id == projects.Id, projects);

        public void Delete(string id) =>
            _projects.DeleteOne(sub => sub.Id == id);
    }
}

