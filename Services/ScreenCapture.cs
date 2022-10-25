using LittleWatcher.Service.Interfaces;
using Serilog;
using System.Drawing;
using System.Drawing.Imaging;

namespace LittleWatcher.Service.Services;

public class ScreenCapture : IScreenCapture
{
    private readonly string FILENAME = "screenshot.jpg";
    private readonly string _path = @"C\temp";
    public async Task CaptureScreen()
    {
        Log.Warning("Screenshot wird erstellt!");
        var captureBmp = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
        using (var captureGraphic = Graphics.FromImage(captureBmp))
        {
            captureGraphic.CopyFromScreen(0, 0, 0, 0, captureBmp.Size);
            var pathFileName = _path + Path.DirectorySeparatorChar + FILENAME;
            captureBmp.Save(pathFileName, ImageFormat.Jpeg);
        }
        Log.Information("Screenshot wurde erstellt!");
    }
    public string GetPathWithFileName()
    {
        return _path + Path.DirectorySeparatorChar + FILENAME;
    }

    public void DeleteFile()
    {
        var file = GetScreenshot();
        if (file != null)
        {
            file.Delete();
            Log.Warning("Screenshot wurde gelöscht");
        }
    }
    public FileInfo GetScreenshot()
    {
        var files = GetDirectoryInfo().GetFiles(FILENAME, SearchOption.AllDirectories);
        return files.FirstOrDefault(x => x.Name.Equals(FILENAME));
    }
    public DirectoryInfo GetDirectoryInfo(string path = "")
    {
        DirectoryInfo dir = new DirectoryInfo(_path);
        if (!dir.Exists)
        {
            dir.Create();
        }
        return dir;
    }
}