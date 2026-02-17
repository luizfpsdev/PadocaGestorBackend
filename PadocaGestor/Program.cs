using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Database;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

const string typeAuthentication = "Bearer";

builder.Services.AddAuthentication(typeAuthentication)
    .AddJwtBearer(typeAuthentication, opt =>
{
    opt.RequireHttpsMetadata = false; //TODO: ajustar para false apenas se for ambiente de desenvolvimento
    opt.SaveToken = true;
    opt.Authority = builder.Configuration.GetSection("AuthConfigs")["AuthServer"];
    opt.Audience = builder.Configuration.GetSection("AuthConfigs")["ClientId"];
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<PadocaContext>(opt=>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Padoca"),b => b.MigrationsAssembly("PadocaGestor.Infrastructure"));
});

var app = builder.Build();

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
