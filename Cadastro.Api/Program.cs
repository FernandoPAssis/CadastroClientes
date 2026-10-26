using Cadastro.Api.Data;                         
using Microsoft.EntityFrameworkCore;
using Cadastro.Api.Messaging;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registra o banco SQLite usando a connection string do appsettings.json
builder.Services.AddDbContext<CadastroDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CadastroDb")));




// >>> RabbitMQ: abre UMA conexão quando a API sobe e reaproveita em todas as requisições
ConnectionFactory fabrica = new ConnectionFactory
{
    HostName = builder.Configuration["RabbitMQ:HostName"] ?? "localhost"
};
IConnection conexaoRabbit = await fabrica.CreateConnectionAsync();

builder.Services.AddSingleton(conexaoRabbit);
builder.Services.AddSingleton<RabbitMqPublisher>();
// <<< RabbitMQ:



var app = builder.Build();

// Cria o arquivo do banco e a tabela na primeira execução
using (var scope = app.Services.CreateScope())
{
    CadastroDbContext db = scope.ServiceProvider.GetRequiredService<CadastroDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
