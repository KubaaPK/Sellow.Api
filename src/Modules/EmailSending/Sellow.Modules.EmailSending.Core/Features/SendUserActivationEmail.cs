using MediatR;
using Microsoft.Extensions.Options;
using Sellow.Modules.Auth.Contracts.IntegrationEvents;
using Sellow.Modules.EmailSending.Core.EmailClients.Sendgrid;
using SendGrid.Helpers.Mail;

namespace Sellow.Modules.EmailSending.Core.Features;

internal sealed class SendUserActivationEmail : INotificationHandler<UserCreated>
{
    private readonly SendgridClient _sendgridClient;
    private readonly SendgridOptions _sendgridOptions;

    public SendUserActivationEmail(SendgridClient sendgridClient, IOptions<SendgridOptions> sendgridOptions)
    {
        _sendgridClient = sendgridClient;
        _sendgridOptions = sendgridOptions.Value;
    }

    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        var email = MailHelper
            .CreateSingleTemplateEmail(
                new EmailAddress("sellow@sellow.io"),
                new EmailAddress(notification.Email),
                _sendgridOptions.Templates.UserActivation.TemplateId,
                new
                {
                    notification.Username,
                    ActivationLink = $"{_sendgridOptions.Templates.UserActivation.ActivationUrl}/{notification.UserId}"
                }
            );

        await _sendgridClient.SendEmail(email, cancellationToken);
    }
}