using Microsoft.AspNetCore.Mvc;
using pokedex.Models;
using pokedex.Services;

namespace pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<List<Pokemon>> Get() =>
            await _pokemonService.GetAllAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Pokemon>> Get(string id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);

            if (pokemon is null)
                return NotFound();

            return pokemon;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pokemon newPokemon)
        {
            await _pokemonService.CreateAsync(newPokemon);
            return CreatedAtAction(nameof(Get), new { id = newPokemon.Id }, newPokemon);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Pokemon updatedPokemon)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);

            if (pokemon is null)
                return NotFound();

            updatedPokemon.Id = pokemon.Id;

            await _pokemonService.UpdateAsync(id, updatedPokemon);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);

            if (pokemon is null)
                return NotFound();

            await _pokemonService.RemoveAsync(pokemon.Id);

            return NoContent();
        }
    }
}