using System.Text.Json.Serialization;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mssql;
using Postgres;
using Shared;
using Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var dbOfChoice = Environment.GetEnvironmentVariable("DB_PROVIDER")!;
switch (dbOfChoice.ToLower())
{
    case "mssql": 
        builder.Services.AddDbContextPool<MssqlContext>(MssqlContextCreator.Apply);
        builder.Services.AddScoped<IDataProvider, MssqlContext>();
        builder.Services.AddHostedService(x => new Migrator<MssqlContext>(x.GetRequiredService<ILogger<Migrator<MssqlContext>>>(), x.GetRequiredService<IServiceScopeFactory>()));
        break;
    case "postgre":
        builder.Services.AddDbContextPool<PostgresContext>(PostgresContextCreator.Apply);
        builder.Services.AddScoped<IDataProvider, PostgresContext>();
        builder.Services.AddHostedService(x => new Migrator<PostgresContext>(x.GetRequiredService<ILogger<Migrator<PostgresContext>>>(), x.GetRequiredService<IServiceScopeFactory>()));
        break;
    case "sqlite":
        builder.Services.AddDbContextPool<SqliteContext>(SqliteContextCreator.Apply);
        builder.Services.AddScoped<IDataProvider, SqliteContext>();
        builder.Services.AddHostedService(x => new Migrator<SqliteContext>(x.GetRequiredService<ILogger<Migrator<SqliteContext>>>(), x.GetRequiredService<IServiceScopeFactory>()));
        break;
    default:
        throw new Exception("Unsupported option. Supported options include ['mssql', 'postgre', 'sqlite']");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/getinfo", ([FromServices] IDataProvider _db) =>
{
    return _db.Members().Include(x => x.Assignations).ThenInclude(x => x.Project);
})
.WithName("GetInfo")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
