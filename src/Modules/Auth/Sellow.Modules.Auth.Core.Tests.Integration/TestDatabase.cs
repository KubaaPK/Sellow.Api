using Microsoft.EntityFrameworkCore;
using Sellow.Modules.Auth.Core.DAL;

namespace Sellow.Modules.Auth.Core.Tests.Integration;

internal sealed class TestDatabase : IDisposable
{
    public AuthDbContext Context { get; }

    public TestDatabase()
    {
        var connectionString =
            $"Server=localhost;Port=5432;Database=Sellow-Auth-Tests-{Guid.NewGuid()};User Id=postgres;Password=postgres;";
        Context = new AuthDbContext(new DbContextOptionsBuilder<AuthDbContext>().UseNpgsql(connectionString).Options);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public Task Init() => Context.Database.EnsureCreatedAsync();

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}