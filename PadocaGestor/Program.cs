using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PadocaGestor.Infrastructure.Database;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PadocaContext>(opt=> opt.UseNpgsql(builder.Configuration.GetConnectionString("Padoca")));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o => o.WithTheme(ScalarTheme.DeepSpace));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
