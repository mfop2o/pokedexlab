using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokedex.Models;
using MongoDB.Driver;

using pokedex.Configuration;
using Microsoft.Extensions.Options;

namespace pokedex.Services
{
    public class PokemonService : IPokemonService
{
    private readonly IMongoCollection<Pokemon> _pokemonsCollection; 

    public PokemonService(IOptions<PokedexDatabaseSettings> pokedexDatabaseSettings)
    {
        var mongoClient = new MongoClient(pokedexDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(pokedexDatabaseSettings.Value.DatabaseName);
        _pokemonsCollection = mongoDatabase.GetCollection<Pokemon>(pokedexDatabaseSettings.Value.PokemonCollectionName);
    }

    public async Task<List<Pokemon>> GetPokemonsAsync()
    {
        return await _pokemonsCollection.Find(_ => true).ToListAsync(); 
    }

    public async Task<Pokemon> GetPokemonByIdAsync(string id)
    {
        return await _pokemonsCollection.Find(pokemon => pokemon.Id == id).FirstOrDefaultAsync(); 
    }

    public async Task<Pokemon> AddPokemonAsync(Pokemon newPokemon)
    {
        await _pokemonsCollection.InsertOneAsync(newPokemon); 
        return newPokemon;
    }

 public async Task<Pokemon> UpdatePokemonAsync(string id, Pokemon updatedPokemon)
{
   
    var existingPokemon = await _pokemonsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    
    if (existingPokemon == null)
    {
        throw new Exception("Pokemon not found");
    }

    // Update fields with values from updatedPokemon
    existingPokemon.Name = updatedPokemon.Name ?? existingPokemon.Name;
    existingPokemon.Type = updatedPokemon.Type ?? existingPokemon.Type;
    existingPokemon.Ability = updatedPokemon.Ability ?? existingPokemon.Ability;
    existingPokemon.Level = updatedPokemon.Level;

    // Update the Pokemon in the database
    await _pokemonsCollection.ReplaceOneAsync(p => p.Id == id, existingPokemon);
    
    return existingPokemon;
}


    public async Task<bool> DeletePokemonAsync(string id)
    {
        var result = await _pokemonsCollection.DeleteOneAsync(pokemon => pokemon.Id == id); 
        return result.DeletedCount > 0;
    }
}

}
