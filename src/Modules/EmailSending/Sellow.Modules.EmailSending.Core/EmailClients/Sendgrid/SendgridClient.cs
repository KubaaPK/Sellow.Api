using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Sellow.Modules.EmailSending.Core.EmailClients.Sendgrid;

internal sealed class SendgridClient
{
    private readonly ISendGridClient _sendGridClient;
    private readonly ILogger<SendgridClient> _logger;

    public SendgridClient(IOptions<SendgridOptions> options, ILogger<SendgridClient> logger)
    {
        _logger = logger;
        _sendGridClient = new SendGridClient(options.Value.ApiKey);
    }

    public async Task SendEmail(SendGridMessage email, CancellationToken cancellationToken = default)
    {
        await _sendGridClient.SendEmailAsync(email, cancellationToken);
        _logger.LogInformation("Email: '{Email}' was sent", email.Serialize());
    }
}