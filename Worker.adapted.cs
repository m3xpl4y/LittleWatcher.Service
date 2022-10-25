using Serilog;

namespace LittleWatcher.Service;

public partial class Worker
{
    private const int MINUTE = 60;
    private const int SECOND = 60;
    private const int MILLISECOND = 1000;
    private async Task<bool> IsIpValid(string ip)
    {
        var newIp = await _ip.GetIp();
        return !ip.Equals(newIp);
    }
    private void IpCheck(out int milliseconds)
    {
        var dailyTime = _settings.TimeOfDay;
        var timeParts = dailyTime.Split(":");

        var dateNow = DateTime.Now;
        var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day,
            int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));

        TimeSpan timeSpan;
        if (date > dateNow)
        {
            timeSpan = date - dateNow;
        }
        else
        {
            date = date.AddDays(1);
            timeSpan = date - dateNow;
        }

        var hoursToMilliSeconds = timeSpan.Hours * MINUTE * SECOND * MILLISECOND;
        var MinutesToMilliSeconds = timeSpan.Minutes * SECOND * MILLISECOND;
        var secondsToMilliSeconds = timeSpan.Seconds * MILLISECOND;
        milliseconds = hoursToMilliSeconds + MinutesToMilliSeconds + secondsToMilliSeconds + timeSpan.Milliseconds;
        Log.Information($"Nächste IP Überprüfung in {timeSpan.Hours} Stunden und {timeSpan.Minutes} Minuten");
    }
}
