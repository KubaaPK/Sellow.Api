using Sellow.Shared.Abstractions.SharedKernel.ValueObjects;

namespace Sellow.Shared.Abstractions.Tests.Unit.SharedKernel.ValueObjects;

public sealed class EmailTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("@")]
    [InlineData("jan")]
    [InlineData("jan@")]
    [InlineData("jan@kowalski")]
    [InlineData("jan@kowalski.")]
    [InlineData("@kowalski.pl")]
    internal void should_not_allow_to_create_an_invalid_email_value_object(string email)
        => Assert.Throws<InvalidEmailException>(() => new Email(email));

    [Theory]
    [InlineData("jan@kowalski.pl")]
    [InlineData("jan-22@kowalski.pl")]
    [InlineData("jan.kowalski@email.com")]
    [InlineData("jan.kowalski@email.co")]
    internal void should_create_a_valid_email_value_object(string email)
        => Assert.Equal(email, new Email(email).Value);
}