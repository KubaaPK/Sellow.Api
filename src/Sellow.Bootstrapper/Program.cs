using Sellow.Modules.Auth.Api;
using Sellow.Modules.EmailSending.Api;
using Sellow.Shared.Infrastructure;
using Sellow.Shared.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args).AddLogging();

builder.Services
    .AddSharedInfrastructure()
    .AddAuthModule()
    .AddEmailSendingModule();

var app = builder.Build();

app.MapControllers();

app
    .UseSharedInfrastructure()
    .UseAuthModule();

app.Run();