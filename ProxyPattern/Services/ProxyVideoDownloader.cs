using ProxyPattern.Interfaces;
using ProxyPattern.Models;

namespace ProxyPattern.Services;

public class ProxyVideoDownloader : VideoDownloader
{
  private Dictionary<string, Video> _videoCache = new();
  private readonly VideoDownloader downloader = new RealVideoDownloader();
  public Video GetVideo(string videoName)
  {
    if (!_videoCache.ContainsKey(videoName)) {
      _videoCache.Add(videoName, downloader.GetVideo(videoName));
    }

    return _videoCache[videoName];
  }
}