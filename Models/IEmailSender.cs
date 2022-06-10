namespace WebApplication4.Models;

public interface IEmailSender
{
    void Send(string senderName, string title, string body, string recipient, CancellationToken cancellationToken);
}