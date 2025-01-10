using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using pokedex.Models;
using pokedex.Services;

namespace pokedex.Controllers
{
    [ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pokemon>>> Get()
    {
        return Ok(await _pokemonService.GetPokemonsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pokemon>> GetPokemon(string id)
    {
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
        if (pokemon == null)
        {
            return NotFound("Pokemon not found");
        }
        return Ok(pokemon);
    }

    [HttpPost]
    public async Task<ActionResult<Pokemon>> AddPokemon(Pokemon newPokemon)
    {
        var pokemon = await _pokemonService.AddPokemonAsync(newPokemon);
        return CreatedAtAction(nameof(GetPokemon), new { id = pokemon.Id }, pokemon);
    }

    [HttpPut("{id}")]
public async Task<ActionResult<Pokemon>> UpdatePokemon(string id, Pokemon updatedPokemon)
{
    try
    {
        
        var pokemon = await _pokemonService.UpdatePokemonAsync(id, updatedPokemon);
        return Ok(pokemon);
    }
    catch (Exception)
    {
        return NotFound("Pokemon not found");
    }
}


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePokemon(string id)
    {
        var success = await _pokemonService.DeletePokemonAsync(id);
        if (!success)
        {
            return NotFound("Pokemon not found");
        }
        return NoContent();
    }
}

}
