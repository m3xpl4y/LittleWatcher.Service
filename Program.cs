using LittleWatcher.Service;
using LittleWatcher.Service.Interfaces;
using LittleWatcher.Service.Models;
using LittleWatcher.Service.Services;
using Serilog;
using Serilog.Events;

string logFileName = "LogFile.txt";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(logFileName)
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .AddEnvironmentVariables();

        var configuration = builder.Build();
        services.Configure<Url>(options => configuration.GetSection("Url").Bind(options));
        services.Configure<EmailSettings>(options => configuration.GetSection("EmailSettings").Bind(options));
        services.Configure<Settings>(options => configuration.GetSection("Settings").Bind(options));

        services.AddHostedService<Worker>();

        services.AddTransient<IScreenCapture, ScreenCapture>();
        services.AddTransient<IIP, IpProvider>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IHtmlService, HtmlService>();
    })
    .Build();
await host.RunAsync();
