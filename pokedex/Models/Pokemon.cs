using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace pokedex.Models
{
    public class Pokemon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("type")]
        public string[] Type { get; set; } = null!;

        [BsonElement("level")]
        public int Level { get; set; }

        [BsonElement("hp")]
        public int HP { get; set; }

        [BsonElement("attack")]
        public int Attack { get; set; }

        [BsonElement("defense")]
        public int Defense { get; set; }
    }
}