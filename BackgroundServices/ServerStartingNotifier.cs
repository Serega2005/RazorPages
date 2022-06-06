using WebApplication4.Models;

namespace WebApplication4.BackgroundServices;

public class ServerStartingNotifier : BackgroundService
{
    private readonly IEmailSender _emailSender;

    public ServerStartingNotifier(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _emailSender.Send(
            "asp2022_5@rodion-m.ru",
            "Сервер запущен",
            "Сервер успешно запущен",
            "Schuganiserega@mail.ru");
    }
}