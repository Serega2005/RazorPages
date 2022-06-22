using System.Text;
using Serilog;
using Serilog.Events;
using WebApplication4.BackgroundServices;
using WebApplication4.Models;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("Starting up");
try
{
    Console.OutputEncoding = Encoding.UTF8;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.Configure<SmtpCredentials>(
        builder.Configuration.GetSection("SmtpCredentials"));
    builder.Services.AddSingleton<ICatalog, InMemoryCatalog>();
    builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
    builder.Services.AddHostedService<ServerStartingNotifier>();

    builder.Host.UseSerilog((ctx, conf) =>
    {
        conf
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
            .ReadFrom.Configuration(ctx.Configuration)
            ;
    });

// Add services to the container.
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush(); //перед выходом дожидаемся пока все логи будут записаны
}

