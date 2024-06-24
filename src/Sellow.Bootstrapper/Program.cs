using Sellow.Shared.Infrastructure;
using Sellow.Shared.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args).AddLogging();

builder.Services
    .AddSharedInfrastructure();

var app = builder.Build();

app
    .UseSharedInfrastructure();

app.Run();