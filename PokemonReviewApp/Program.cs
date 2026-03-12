using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;
using System.Text.Json.Serialization;

// This file is the entry point of the application, it is responsible for configuring services and the app itself.
// It sets up the dependency injection, configures the database connection, and defines the HTTP request pipeline.

var builder = WebApplication.CreateBuilder(args); // Create a builder for the web application, a builder is used to configure services and the app

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>(); // Seed service, Dependency Injection
builder.Services.AddControllers().AddJsonOptions(x => 
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); // This line configures the JSON serialization options for the controllers.
                                                                           // It sets the ReferenceHandler to IgnoreCycles, which means that if there are
                                                                           // circular references in the object graph being serialized, it will ignore
                                                                           // them instead of throwing an exception. This is useful to prevent issues
                                                                           // when serializing complex objects that may have circular references.
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>(); // Dependency Injection, tells the app that whenever IPokemonRepository is requested, it should provide
                                                                     // an instance of PokemonRepository. The scoped lifetime means that a new instance of PokemonRepository will be
                                                                     // created for each HTTP request and shared within that request.
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); 
builder.Services.AddScoped<ICountryRepository, CountryRepository>(); 
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());                                                                     
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
