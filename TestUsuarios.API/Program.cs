using Microsoft.OpenApi.Models;
using System;
using TestUsuarios.API;
using TestUsuarios.Lib.Uri;
using TesUsuarios.Data.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string title = "Api Test Usuarios";
string version = "1.0";
string description = $"{title} {version} - Knowledge Test.";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = version });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IUriLib>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext?.Request;
    var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
    return new UriLib(uri);
});

//add Dependency Injection
builder.Services.AddDependency();
//add DataBase in Memory
builder.Services.AddDbContext<DatabaseContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", description);
    });
}
app.UseCors(option =>
{
    option.WithOrigins("*");
    option.AllowAnyMethod();
    option.AllowAnyHeader();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
