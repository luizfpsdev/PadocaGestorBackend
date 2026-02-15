using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PadocaGestor.Infrastructure.Database;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

const string typeAuthentication = "Bearer";

// Add services to the container.


builder.Services.AddAuthentication(typeAuthentication)
    .AddJwtBearer(typeAuthentication, opt =>
{
    opt.RequireHttpsMetadata = false; //TODO: ajustar para false apenas se for ambiente de desenvolvimento
    opt.SaveToken = true;
    opt.Authority = builder.Configuration.GetSection("AuthConfigs")["AuthServer"];
    opt.Audience = builder.Configuration.GetSection("AuthConfigs")["ClientId"];
    
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidIssuer = builder.Configuration.GetSection("AuthConfigs")["AuthServer"]
    };
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PadocaContext>(opt=> opt.UseNpgsql(builder.Configuration.GetConnectionString("Padoca")));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
    {
        o.WithTheme(ScalarTheme.Saturn);
        o.HideClientButton = true;
        o.HideDarkModeToggle = true;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
