using System.Net;
using System.Net.Mail;

namespace WebApplication4.Models;

public class SmtpEmailSender : IEmailSender
{
    private SmtpClient _smtpClient;

    public SmtpEmailSender()
    {
        _smtpClient = new SmtpClient("smtp.beget.com")
        {
            Port = 25,
            Credentials = new NetworkCredential(
                "asp2022_5@rodion-m.ru",
                "J8S*U*b&"
            ),
        };
    }

    public void Send(string senderEmail, string title, string body, string recipient)
    {
        _smtpClient.Send(
            recipients: recipient,
            body: body,
            subject: title,
            from: senderEmail
        );
    }
}