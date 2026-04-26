using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PadocaGestor.Api;
using PadocaGestor.Api.Services.Fornecedores;
using PadocaGestor.Application;
using PadocaGestor.Application.Abstractions;
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
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<PadocaContext>(opt=>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Padoca"),b => b.MigrationsAssembly("PadocaGestor.Infrastructure"));
});

//Registro de serviços
builder.Services.TryAddScoped<IUsuarioAtual,UsuarioAtual>();
builder.Services.TryAddScoped<IUsuarioClienteService,UsuarioClienteService>();
builder.Services.TryAddScoped<IFornecedorCadastroValidator,FornecedorCadastroValidator>();
builder.Services.TryAddScoped<IFornecedoresService,FornecedoresService>();


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
