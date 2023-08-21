using Microsoft.Extensions.Configuration;
using NetCoreYARPEntegration.APIGATEWAY;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// YARP AddReverseProxy
if (_configuration.GetValue<bool>("UseCodeBasedConfig"))
{
    builder.Services.AddSingleton<IProxyConfigProvider>(new YarpCustomProxyConfiguration()).AddReverseProxy();
}
else
{
    builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
}

var app = builder.Build();

// YAPR Add MapReverseProxy
app.MapReverseProxy();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
