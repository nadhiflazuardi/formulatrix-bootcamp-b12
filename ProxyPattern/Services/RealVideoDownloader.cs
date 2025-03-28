using ProxyPattern.Interfaces;
using ProxyPattern.Models;

namespace ProxyPattern.Services;

public class RealVideoDownloader : VideoDownloader {
  public Video GetVideo(string videoName) {
    Console.WriteLine("Connecting to https://www.youtube.com/");
    Console.WriteLine("Downloading video...");
    Console.WriteLine($"Success! Downloaded video: {videoName}");
    return new Video(videoName);
  }
}