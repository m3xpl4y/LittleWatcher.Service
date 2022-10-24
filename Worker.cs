using LittleWatcher.Service.Interfaces;
using LittleWatcher.Service.Models;
using Microsoft.Extensions.Options;
using Serilog;

namespace LittleWatcher.Service
{
    public partial class Worker : BackgroundService
    {
        private readonly IIP _ip;
        private readonly IScreenCapture _screenCapture;
        private readonly IEmailService _emailService;
        private readonly IHtmlService _htmlService;
        private readonly IOptions<Settings> _settings;

        public Worker(IIP ip, IScreenCapture screenCapture, IEmailService emailService, 
            IHtmlService htmlService, IOptions<Settings> settings)
        {
            _ip = ip;
            _screenCapture = screenCapture;
            _emailService = emailService;
            _htmlService = htmlService;
            _settings = settings;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var oldIp = "0.0.0.0";
            while (!stoppingToken.IsCancellationRequested)
            {
                if (await IsIpValid(oldIp))
                {
                    Log.Information("IP wird aufgerufen!");
                    var ip = await _ip.GetIp();
                    Log.Information($"IP lautet {ip}!");
                    await _screenCapture.CaptureScreen();
                    await _emailService.SendMail(_htmlService.Subject("Little Watcher"),
                                                 _htmlService.Body(ip), "Little Watcher");
                    _screenCapture.DeleteFile();
                    Log.Warning("IP �nderung fand statt!");
                    oldIp = ip;
                }
                else
                {
                    Log.Information("IP wurde nicht ge�ndert!");
                }
                IpCheck(out var milliseconds);
                await Task.Delay(milliseconds, stoppingToken);
            }
        }
    }
}