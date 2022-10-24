namespace LittleWatcher.Service.Interfaces;

public interface IHtmlService
{
    string Body(string ip, string additionalText = "");
    string Subject(string subject);
}
