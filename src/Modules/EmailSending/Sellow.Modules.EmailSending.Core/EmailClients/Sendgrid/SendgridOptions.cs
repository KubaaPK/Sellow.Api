namespace Sellow.Modules.EmailSending.Core.EmailClients.Sendgrid;

internal sealed class SendgridOptions
{
    public string? ApiKey { get; set; }
    public EmailTemplates Templates { get; set; } = new();

    internal sealed class EmailTemplates
    {
        public UserActivationTemplate UserActivation { get; set; } = new();

        internal sealed class UserActivationTemplate
        {
            public string? TemplateId { get; set; }
            public string? ActivationUrl { get; set; }
        }
    }
}