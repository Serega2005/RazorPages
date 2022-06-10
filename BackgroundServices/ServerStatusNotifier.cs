using WebApplication4.Models;

namespace WebApplication4.BackgroundServices;

public class ServerStatusNotifier : BackgroundService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<ServerStatusNotifier> _logger;

    public ServerStatusNotifier(IEmailSender? emailSender, ILogger<ServerStatusNotifier> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogDebug("Пытаемся отправить письмо");
            _emailSender.Send(
                "asp2022_5@rodion-m.ru",
                "Статус сервера",
                "Норм",
                "Schuganiserega@mail.ru");
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}