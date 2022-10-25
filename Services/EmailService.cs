using LittleWatcher.Service.Interfaces;
using LittleWatcher.Service.Models;
using Serilog;
using System.Net.Mail;

namespace LittleWatcher.Service.Services;

public class EmailService : IEmailService
{
    private readonly IScreenCapture _screenCapture;
    private readonly EmailSettings _emailSettings;

    public EmailService(IScreenCapture screenCapture, EmailSettings emailSettings)
    {
        _screenCapture = screenCapture;
        _emailSettings = emailSettings;
    }
    public async Task SendMail(string subject, string message, string displayName)
    {
        Log.Information("Email wird gesendet!");
        try
        {
            SmtpClient sc = new SmtpClient();
            sc.Host = _emailSettings.Host;
            sc.Port = _emailSettings.Port;
            sc.EnableSsl = false;
            sc.Credentials = new System.Net.NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            Attachment att = new Attachment(_screenCapture.GetPathWithFileName());
            MailMessage msg = new MailMessage();
            msg.Attachments.Add(att);

            msg.From = new MailAddress(_emailSettings.Email, displayName);
            msg.Subject = subject;
            msg.To.Add(new MailAddress(_emailSettings.EmailRecipient));
            msg.Body = message;
            msg.IsBodyHtml = true;
            sc.Send(msg);
            att.Dispose();
        }
        catch (Exception ex)
        {
            Log.Error("Email konnte nicht gesendet werden!", ex.Message);
        }
        Log.Information("Email wurde gesendet!");
    }
}
