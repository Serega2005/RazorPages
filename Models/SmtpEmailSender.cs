using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace WebApplication4.Models;

public class SmtpEmailSender : IEmailSender
{
    private readonly ILogger<SmtpEmailSender> _logger;
    private SmtpCredentials _smtpCredentials;
    
    public SmtpEmailSender(IOptionsSnapshot<SmtpCredentials> options, ILogger<SmtpEmailSender> logger)
    {
        _logger = logger;
        _smtpCredentials = options.Value;
    }
    
    public void Send(string senderEmail, string title, string body, string recipient)
    {
        var smtpClient = new SmtpClient(_smtpCredentials.Host)
        {
            Port = _smtpCredentials.Port,
            Credentials = new NetworkCredential(
                _smtpCredentials.UserName,
                _smtpCredentials.Password
            ),
        };
    
        smtpClient.Send(
            recipients: recipient,
            body: body,
            subject: title,
            from: senderEmail
        );
    }
}

public class SmtpCredentials
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Host { get; set; }
    public int Port { get; set; }
}