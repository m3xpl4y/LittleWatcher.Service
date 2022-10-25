using LittleWatcher.Service;
using LittleWatcher.Service.Interfaces;
using LittleWatcher.Service.Models;
using LittleWatcher.Service.Services;
using Serilog;
using Serilog.Events;

string logFileName = @"C\temp\LogFile.txt";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(logFileName)
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        Url url = configuration.GetSection("Url").Get<Url>();
        EmailSettings emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        Settings settings = configuration.GetSection("Settings").Get<Settings>();

        services.AddSingleton(url);
        services.AddSingleton(emailSettings);
        services.AddSingleton(settings);

        services.AddHostedService<Worker>();

        services.AddTransient<IScreenCapture, ScreenCapture>();
        services.AddTransient<IIP, IpProvider>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IHtmlService, HtmlService>();
    })
    .Build();
await host.RunAsync();
