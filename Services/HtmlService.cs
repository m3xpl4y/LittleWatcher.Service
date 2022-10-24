using LittleWatcher.Service.Interfaces;
using System.Text;

namespace LittleWatcher.Service.Services;

public class HtmlService : IHtmlService
{
    public string Body(string ip, string additionalText = "")
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<html>");
        sb.AppendLine("<body>");
        sb.AppendLine("<H1>");
        sb.AppendLine($"<H1>{ip}</H1>");
        sb.AppendLine("</H1>");
        sb.AppendLine($"<p>{additionalText}</p>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }
    public string Subject(string subject)
    {
        var sb = new StringBuilder();
        sb.Append(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
        sb.Append($" {subject}");
        return sb.ToString();
    }
}
