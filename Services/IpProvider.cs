using LittleWatcher.Service.Interfaces;
using LittleWatcher.Service.Models;
using System.Net;

namespace LittleWatcher.Service.Services;

public class IpProvider : IIP
{
    private readonly Url _url;

    public IpProvider(Url url)
    {
        _url = url;
    }

    public async Task<string> GetIp()
    {
        string response = UrlResponse();
        string[] ipAddressWithText = response.Split(':');
        string ipAddressWithHTMLEnd = ipAddressWithText[1].Substring(1);
        string[] ipAddress = ipAddressWithHTMLEnd.Split('<');
        return ipAddress[0];
    }

    private string UrlResponse()
    {
        var url = new Url()
        {
            Website = _url.Website
        };
        WebRequest request = WebRequest.Create(url.Website);
        WebResponse response = request.GetResponse();
        StreamReader streamReader = new StreamReader(response.GetResponseStream());
        return streamReader.ReadToEnd().Trim();
    }
}
