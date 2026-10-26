using Cadastro.Api.Data;                         
using Microsoft.EntityFrameworkCore;             

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registra o banco SQLite usando a connection string do appsettings.json
builder.Services.AddDbContext<CadastroDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CadastroDb")));


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
