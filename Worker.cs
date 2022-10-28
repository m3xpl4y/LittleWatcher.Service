using LittleWatcher.Service.Interfaces;
using LittleWatcher.Service.Models;
using LittleWatcher.Service.Services;
using Serilog;

namespace LittleWatcher.Service
{
    public partial class Worker : BackgroundService
    {
        private readonly IIP _ip;
        private readonly IEmailService _emailService;
        private readonly IHtmlService _htmlService;
        private readonly Settings _settings;

        public Worker(IIP ip, IEmailService emailService, 
            IHtmlService htmlService, Settings settings)
        {
            _ip = ip;
            _emailService = emailService;
            _htmlService = htmlService;
            _settings = settings;
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
                    await _emailService.SendMail(_htmlService.Subject("Little Watcher", SystemInformation.UserName),
                                                 _htmlService.Body(ip), "Little Watcher");

                    oldIp = ip;
                }
                else
                {
                    Log.Information("IP wurde nicht geändert!");
                }
                IpCheck(out var milliseconds);
                await Task.Delay(milliseconds, stoppingToken);
            }
        }
    }
}