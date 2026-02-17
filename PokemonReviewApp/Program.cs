using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;

var builder = WebApplication.CreateBuilder(args); // Create a builder for the web application, a builder is used to configure services and the app

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>(); // Seed service, Dependency Injection
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>(); // Dependency Injection, tells the app that whenever
                                                                     // it sees IPokemonRepository, it should use PokemonRepository as the implementation
// Transient means create a new instance every time it's requested
// Scoped means create one instance per request
// Singleton means create one instance for the entire application lifetime

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => // 6
{ // Telling our DataContext/DbContext how to connect to our DB
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

var app = builder.Build(); // Build the web application

if (args.Length == 1 && args[0].ToLower() == "seeddata") // Seed data if the argument is passed
    SeedData(app);
void SeedData(IHost app) // Injecting service into Program.cs
{
    var scopedFactory = app.Services.GetRequiredService<IServiceScopeFactory>(); // Create a scope to get the service

    using (var scope = scopedFactory.CreateScope()) // Create a scope
    {
        var service = scope.ServiceProvider.GetRequiredService<Seed>(); // Get the Seed service
        service.SeedDataContext(); // Call the SeedDataContext method to seed the database
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
