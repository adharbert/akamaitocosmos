using Application.Services;
using Domain.Models.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Repositories;
using System.Text;



var builder = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json");



Console.Title = "Getting Started with the Cosmos DB Provider";
Console.WriteLine("Launching...");


var config = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile($"appsettings.json").Build();

AkamaiApiOptions AkamaiConfig = new AkamaiApiOptions();
config.GetSection("akamaiService:development").Bind(AkamaiConfig);

string logCredSource = $"{AkamaiConfig.ClientId}:{AkamaiConfig.ClientSecret}";
string encryptSource = Convert.ToBase64String(Encoding.UTF8.GetBytes(logCredSource));
string headerSource = $"Basic {encryptSource}";


// Call CosmosDB connectionString
var cosmosConnectionString = config["cosmosService:CosmosConnectionString"];
var cosmosDatabase = config["cosmosService:DatabaseName"];
var containerName = config["cosmosService:ContainerName"];


// Services
var services = new ServiceCollection();

//services.AddScoped(typeof(IGenericRepository<>), typeof(ApiAkamaiGenericRepository<>));
services.AddScoped<IRepository, ApiAkamaiRepository>();
services.AddScoped<ICosmosRepository, UserComosRepository>();


services.AddHttpClient("SourceAPI", client => {
    client.DefaultRequestHeaders.Clear();
    //var contentType = new ProductInfoHeaderValue("Content-Type", "application/json");      //  ProductInfoHeaderValue("Content-Type", $"application/x-www-form-urlencoded");
    //var auth = new ProductHeaderValue("Authorization", headerSource);
    client.BaseAddress = AkamaiConfig.Url;
    //client.DefaultRequestHeaders.UserAgent.Add(contentType);
    client.DefaultRequestHeaders.Add("Authorization", headerSource);
    //client.DefaultRequestHeaders.Add("Content-Type", "application//x-www-form-urlencoded");
});




// DbContext Factor
services.AddDbContextFactory<AkamaiContext>(optionsBuilder =>
  optionsBuilder
    .UseCosmos(
      connectionString: cosmosConnectionString,
      databaseName: cosmosDatabase,
      cosmosOptionsAction: options =>
      {
          options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Direct);
          options.MaxRequestsPerTcpConnection(20);
          options.MaxTcpConnectionsPerEndpoint(32);
      }));

var client = new CosmosClient(cosmosConnectionString);
var database = await client.CreateDatabaseIfNotExistsAsync(cosmosDatabase);
//await database.CreateContainerIfNotExistsAsync(containerName, "/uuid");



//using var serviceProvider = services.BuildServiceProvider();
//var transportServices = serviceProvider.GetRequiredService<AkamaiService>();



/**/
services.AddTransient<MigrateDataService>();
services.AddSingleton<Application.Services.WriteLine>((text, highlight, isException) =>
{
    if (isException)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    else if (highlight)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    Console.WriteLine(text);
    Console.ResetColor();
});
using var serviceProvider = services.BuildServiceProvider();
var migrateService = serviceProvider.GetRequiredService<MigrateDataService>();
await migrateService.Transfer();



Console.WriteLine();
Console.WriteLine("Done. Press ENTER to quit.");
Console.ReadLine();


