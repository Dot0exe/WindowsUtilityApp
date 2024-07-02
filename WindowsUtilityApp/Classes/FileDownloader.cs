using System.Net;

public class FileDownloader
{
    public string DownloadFile(string url, string downloadPath)
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, downloadPath);
        }
        return downloadPath;
    }
}