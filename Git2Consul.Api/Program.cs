using System.Reflection;
using Git2Consul.Api.Extensions;
using Git2Consul.Api.Middlewares;
using Git2Consul.ApplicationCore.Abstract;
using Git2Consul.ApplicationCore.Services;
using Microsoft.OpenApi.Models;
using static Git2Consul.Api.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyedSingleton(
    AppName,
    builder.Configuration.GetByPath<string>(AppName));
builder.Services.AddKeyedSingleton(
    BaseLocalPath,
    builder.Configuration.GetByPath<string>(BaseLocalPath));
builder.Services.AddGit2ConsulEnvironments(builder.Configuration);
builder.Services.AddScoped<ISyncService, SyncService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Configuration.GetValue<string>(AppName) });
});

var app = builder.Build();

app.UseExceptionHandler();
app.MapEndpoints();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var appName = app.Services.GetRequiredKeyedService<string>(AppName);
    options.DocumentTitle = appName;
    options.SwaggerEndpoint("/swagger/v1/swagger.json", appName);
});

await app.RunAsync();
