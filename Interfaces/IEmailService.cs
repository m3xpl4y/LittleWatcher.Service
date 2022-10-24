namespace LittleWatcher.Service.Interfaces;

public interface IEmailService
{
    Task SendMail(string subject, string message, string displayName);
}
