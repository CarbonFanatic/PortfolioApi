using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApi.Models
{
    public class Career
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string WorkplaceName { get; set; }

        public string Role { get; set; }

        public string ActiveYears { get; set; }

        public string Description { get; set; }

    }
}
