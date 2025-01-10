using pokedex.Services;
using pokedex.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PokedexDatabaseSettings>(
    builder.Configuration.GetSection("PokedexDatabaseSettings"));

builder.Services.AddScoped<IPokemonService, PokemonService>(); 

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseAuthorization();

app.MapControllers();

app.Run();
