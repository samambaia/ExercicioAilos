using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Application.Handlers;
using Questao5.Application.Interfaces;
using Questao5.Infrastructure.Repositories;
using Questao5.Infrastructure.Sqlite;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register database configuration and bootstrap
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Register repositories
builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
builder.Services.AddScoped<IMovimentoRepository, MovimentoRepository>();
builder.Services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();

// Register IMediatorHandler
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

// Register MediatR and specify the assembly containing the handlers
builder.Services.AddMediatR(typeof(MovimentacaoCommandHandler).Assembly);

// Register IDbConnection for Dapper
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<DatabaseConfig>();
    return new SqliteConnection(config.Name);
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Initialize the database
var databaseBootstrap = app.Services.GetService<IDatabaseBootstrap>();
if (databaseBootstrap != null)
{
    databaseBootstrap.Setup();
}
else
{
    throw new InvalidOperationException("Database bootstrap service is not registered.");
}

app.Run();

// Informa��es �teis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html
