using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace pokedex.Models
{
   public class Pokemon
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }  // Make the Id nullable

    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Ability { get; set; }
    public int Level { get; set; }
}

}