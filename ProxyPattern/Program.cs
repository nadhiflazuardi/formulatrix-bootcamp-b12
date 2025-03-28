using ProxyPattern.Interfaces;
using ProxyPattern.Services;

class Program
{
  static void Main()
  {
    VideoDownloader videoDownloader = new ProxyVideoDownloader();
    videoDownloader.GetVideo("Proxy Pattern");
    videoDownloader.GetVideo("Proxy Pattern");
    videoDownloader.GetVideo("Another Pattern");
    videoDownloader.GetVideo("Proxy Pattern");
    videoDownloader.GetVideo("Another Pattern");
  }
}