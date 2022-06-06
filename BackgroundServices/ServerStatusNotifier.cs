using WebApplication4.Models;

namespace WebApplication4.BackgroundServices;

public class ServerStatusNotifier : BackgroundService
{
    private readonly IEmailSender _emailSender;

    public ServerStatusNotifier(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _emailSender.Send(
                "asp2022_5@rodion-m.ru",
                "Статус сервера",
                "Норм",
                "Schuganiserega@mail.ru");
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}