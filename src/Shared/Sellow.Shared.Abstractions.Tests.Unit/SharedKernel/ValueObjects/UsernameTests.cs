using Sellow.Shared.Abstractions.SharedKernel.ValueObjects;

namespace Sellow.Shared.Abstractions.Tests.Unit.SharedKernel.ValueObjects;

public sealed class UsernameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData("ab")]
    [InlineData("waaaaaaaaaaaaaaaaaa-to-long-username")]
    [InlineData("        ab        ")]
    [InlineData("    ab")]
    internal void should_not_allow_to_create_an_invalid_username_value_object(string username)
        => Assert.Throws<InvalidUsernameException>(() => new Username(username));

    [Theory]
    [InlineData("user.name.22")]
    [InlineData("abc")]
    [InlineData("exact-maximum-length!!!!!")]
    internal void should_create_a_valid_username_value_object(string username)
        => Assert.Equal(username, new Username(username).Value);

    [Fact]
    internal void should_trim_all_whitespaces_in_username()
    {
        var username = new Username("use rname with whi tespa ces  ");

        Assert.Equal("usernamewithwhitespaces", username.Value);
    }
}