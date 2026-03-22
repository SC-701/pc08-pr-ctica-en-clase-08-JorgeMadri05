using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Seguridad;
using Autorizacion.Abstracciones.DA;
using Autorizacion.Abstracciones.Flujo;
using Autorizacion.DA;
using Autorizacion.DA.Repositorios;
using Autorizacion.Flujo;
using Autorizacion.Middleware;
using DA;
using DA.Repositarios;
using Flujo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Reglas;
using Servicios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IConfiguracion, Configuracion>();
builder.Services.AddControllers();

var tokenConfig = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfig.Issuer,
            ValidAudience = tokenConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.key))
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();
builder.Services.AddScoped<ICategoriaFlujo, CategoriaFlujo>();
builder.Services.AddScoped<ISubCategoriaFlujo, SubCategoriaFlujo>();
builder.Services.AddScoped<IProductoDA, ProductoDA>();
builder.Services.AddScoped<ICategoriaDA, CategoriaDA>();
builder.Services.AddScoped<ISubCategoriaDA, SubCategoriaDA>();
builder.Services.AddScoped<IRepositarioDapper, RepositarioDapper>();
builder.Services.AddScoped<IProductoReglas, ProductoReglas>();
builder.Services.AddScoped<ITipoCambioServicio, TipoCambioServicio>();
builder.Services.AddTransient<IAutorizacionFlujo, AutorizacionFlujo>();
builder.Services.AddTransient<ISeguridadDA, SeguridadDA>();
builder.Services.AddTransient<IRepositorioDapper, RepositorioDapper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AutorizacionClaims();
app.UseAuthorization();

app.MapControllers();

app.Run();
