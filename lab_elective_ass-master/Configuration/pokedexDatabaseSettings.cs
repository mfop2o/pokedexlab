namespace pokedex.Configuration
{
    public class PokedexDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PokemonCollectionName { get; set; } = null!;
    }
}
