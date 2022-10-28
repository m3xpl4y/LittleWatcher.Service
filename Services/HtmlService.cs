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
        sb.Append("<p>");
        sb.AppendLine($"<b>OS:</b> {SystemInformation.OperationSystem} </br>"); 
        sb.AppendLine($"<b>Prozess Architektur:</b> {SystemInformation.ProcessorArchitecture}</br>");
        sb.AppendLine($"<b>Prozess Model:</b> {SystemInformation.ProcessorModel}</br>");
        sb.AppendLine($"<b>UserDomainName:</b> {SystemInformation.UserDomainName}</br>");
        sb.AppendLine($"<b>UserName:</b> {SystemInformation.UserName} </br>");
        sb.Append("</p>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }
    public string Subject(string subject, string additionalText = "")
    {
        var sb = new StringBuilder();
        sb.Append(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
        sb.Append($" {subject} {additionalText}");
        return sb.ToString();
    }
}
