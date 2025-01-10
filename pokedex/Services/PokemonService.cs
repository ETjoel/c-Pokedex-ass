using Microsoft.Extensions.Options;
using MongoDB.Driver;
using pokedex.Models;

namespace pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _pokemonCollection = mongoDatabase.GetCollection<Pokemon>(
                mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Pokemon>> GetAllAsync() =>
            await _pokemonCollection.Find(_ => true).ToListAsync();

        public async Task<Pokemon?> GetByIdAsync(string id) =>
            await _pokemonCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pokemon pokemon) =>
            await _pokemonCollection.InsertOneAsync(pokemon);

        public async Task UpdateAsync(string id, Pokemon pokemon) =>
            await _pokemonCollection.ReplaceOneAsync(x => x.Id == id, pokemon);

        public async Task RemoveAsync(string id) =>
            await _pokemonCollection.DeleteOneAsync(x => x.Id == id);
    }
}