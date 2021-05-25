using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PortfolioApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<Models.User> _user;
        private readonly string key;

        public UserService(Models.IDatabaseSettings settings,IConfiguration configuration)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>("User");
            this.key = configuration.GetSection("JwtKey").ToString();
        }

        public User Create(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public List<User> Read() =>
            _user.Find(sub => true).ToList();

        public User Find(string id) =>
            _user.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(string id, User user) =>
            _user.ReplaceOne(sub => sub.Id == user.Id, user);

        public void Delete(string id) =>
            _user.DeleteOne(sub => sub.Id == id);

        public string Authenticate(string email, string password)
        {
            var user = this._user.Find(x => x.Email == email && x.Password == password).FirstOrDefault();

            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {

                    new Claim(ClaimTypes.Email, email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
