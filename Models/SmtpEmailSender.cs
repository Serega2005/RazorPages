using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace WebApplication4.Models;

public class SmtpEmailSender : IEmailSender
{
    private readonly ILogger<SmtpEmailSender> _logger;
    private readonly SmtpCredentials _smtpCredentials;

    public SmtpEmailSender(IOptionsSnapshot<SmtpCredentials> options, ILogger<SmtpEmailSender> logger)
    {
        _logger = logger;
        _smtpCredentials = options.Value;
    }

    public async Task Send(string senderEmail, string title, string body, string recipient,
        CancellationToken cancellationToken)
    {
        if (senderEmail == null) throw new ArgumentNullException(nameof(senderEmail));
        if (title == null) throw new ArgumentNullException(nameof(title));
        if (body == null) throw new ArgumentNullException(nameof(body));
        if (recipient == null) throw new ArgumentNullException(nameof(recipient));

        _logger.LogDebug("Пытаемся отправить письмо");

        var smtpClient = new SmtpClient(_smtpCredentials.Host) //"smtp.beget.com"
        {
            Port = _smtpCredentials.Port, //25
            Credentials = new NetworkCredential(
                _smtpCredentials.UserName,
                _smtpCredentials.Password
            ),
        };

        await smtpClient.SendMailAsync(
            recipients: recipient,
            body: body,
            subject: title,
            from: senderEmail,
            cancellationToken: cancellationToken
        );
    }

    public class SmtpCredentials
    {
        public string? UserName { get; set; } = "asds@gmail.com";
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
    }
}