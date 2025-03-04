using System.Drawing;

enum PointValues
{
  Miss = 0,
  Bronze = 10,
  Silver = 25,
  Gold = 50,
}

class Program
{
  static void Main()
  {
    PointValues[] hits = [
      PointValues.Gold,
      PointValues.Silver,
      PointValues.Miss,
      PointValues.Silver,
      PointValues.Bronze
    ];

    int totalScore = 0;
    foreach (PointValues hit in hits) {
      totalScore += (int) hit;
    }

    Console.WriteLine($"Total score: {totalScore}");
  }
}