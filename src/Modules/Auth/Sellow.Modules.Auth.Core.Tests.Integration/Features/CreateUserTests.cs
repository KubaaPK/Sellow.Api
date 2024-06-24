using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Sellow.Modules.Auth.Contracts.IntegrationEvents;
using Sellow.Modules.Auth.Core.Auth;
using Sellow.Modules.Auth.Core.DAL.Repositories;
using Sellow.Modules.Auth.Core.Domain;
using Sellow.Modules.Auth.Core.Features;

namespace Sellow.Modules.Auth.Core.Tests.Integration.Features;

public sealed class CreateUserTests : IDisposable
{
    private Task<Guid> Act(CreateUser command) => _handler.Handle(command, default);

    [Fact]
    internal async Task should_add_a_new_user_to_the_database()
    {
        await _testDatabase.Init();
        var command = new CreateUser("jan@kowalski.pl", "jankowalski", "jankowalski");

        await Act(command);

        Assert.Equal(1, _testDatabase.Context.Users.Count());
    }

    [Fact]
    internal async Task should_not_allow_to_create_duplicated_user()
    {
        await _testDatabase.Init();
        var command = new CreateUser("jan@kowalski.pl", "jankowalski", "jankowalski");
        await Act(command);

        await Assert.ThrowsAsync<UserAlreadyExistsException>(() => Act(command));
    }

    [Fact]
    internal async Task should_publish_an_event_when_user_was_created()
    {
        await _testDatabase.Init();
        var command = new CreateUser("jan@kowalski.pl", "jankowalski", "jankowalski");
        await Act(command);

        await _publisher.Received(1).Publish(Arg.Any<UserCreated>());
    }

    [Fact]
    internal async Task should_delete_user_from_the_database_if_creation_in_external_auth_fails()
    {
        await _testDatabase.Init();
        var command = new CreateUser("jan@kowalski.pl", "jankowalski", "jankowalski");
        _authService.CreateUser(Arg.Any<ExternalAuthUser>()).ThrowsAsync(new Exception());

        _ = await Record.ExceptionAsync(() => Act(command));

        Assert.Equal(0, _testDatabase.Context.Users.Count());
    }

    #region Arrange

    private readonly TestDatabase _testDatabase;
    private readonly CreateUserHandler _handler;
    private readonly IPublisher _publisher = Substitute.For<IPublisher>();
    private readonly IAuthService _authService = Substitute.For<IAuthService>();

    public CreateUserTests()
    {
        _testDatabase = new TestDatabase();
        IUserRepository userRepository = new UserRepository(_testDatabase.Context);
        _handler = new CreateUserHandler(
            Substitute.For<ILogger<CreateUserHandler>>(),
            userRepository,
            _publisher,
            _authService
        );
    }

    #endregion

    public void Dispose() => _testDatabase.Dispose();
}