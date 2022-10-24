using LittleWatcher.Service.Interfaces;
using LittleWatcher.Service.Models;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net.Mail;

namespace LittleWatcher.Service.Services;

public class EmailService : IEmailService
{
    private readonly IScreenCapture _screenCapture;
    private readonly IOptions<EmailSettings> _emailSettings;

    public EmailService(IScreenCapture screenCapture, IIP ip, IOptions<EmailSettings> emailSettings)
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
            sc.Host = _emailSettings.Value.Host;
            sc.Port = _emailSettings.Value.Port;
            sc.EnableSsl = false;
            sc.Credentials = new System.Net.NetworkCredential(_emailSettings.Value.Email, _emailSettings.Value.Password);
            Attachment att = new Attachment(_screenCapture.GetPathWithFileName());
            MailMessage msg = new MailMessage();
            msg.Attachments.Add(att);

            msg.From = new MailAddress(_emailSettings.Value.Email, displayName);
            msg.Subject = subject;
            msg.To.Add(new MailAddress(_emailSettings.Value.EmailRecipient));
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
