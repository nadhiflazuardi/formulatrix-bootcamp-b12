using ProxyPattern.Models;

namespace ProxyPattern.Interfaces;

public interface VideoDownloader
{
  public Video GetVideo(string videoName);
}