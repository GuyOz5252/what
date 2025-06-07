using System.Reflection;
using Git2Consul.Api.Extensions;
using Git2Consul.Api.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGit2ConsulEnvironments(builder.Configuration);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Configuration.GetValue<string>("AppName") });
});

var app = builder.Build();

app.UseCors();
app.UseExceptionHandler();
app.MapEndpoints();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var appName = builder.Configuration.GetValue<string>("AppName");
    options.DocumentTitle = appName;
    options.SwaggerEndpoint("/swagger/v1/swagger.json", appName);
});

await app.RunAsync();
