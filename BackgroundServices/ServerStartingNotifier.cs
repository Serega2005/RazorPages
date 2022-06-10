using WebApplication4.Models;

namespace WebApplication4.BackgroundServices;

public class ServerStartingNotifier : BackgroundService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<ServerStartingNotifier> _logger;

    public ServerStartingNotifier(IEmailSender? emailSender, ILogger<ServerStartingNotifier> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Сервер запущен");
        _emailSender.Send(
            "asp2022_5@rodion-m.ru",
            "Сервер запущен",
            "Сервер успешно запущен",
            "Schuganiserega@mail.ru");
    }
}