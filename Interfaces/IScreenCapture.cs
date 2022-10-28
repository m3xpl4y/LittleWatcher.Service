namespace LittleWatcher.Service.Interfaces;

public interface IScreenCapture
{
    Task CaptureScreen();
    string GetPathWithFileName();
    FileInfo GetScreenshot();
    void DeleteFile();
    DirectoryInfo GetDirectoryInfo(string path = "");
}
