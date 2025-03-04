class Program
{
  static void DisplayDictionary(Dictionary<string, int> dict)
  {
    foreach (var pair in dict)
    {
      Console.WriteLine($"{pair.Key}: Power Level {pair.Value}");
    }
  }
  
  static void Main()
  {
    Dictionary<string, int> characters = new();

    characters.Add("Mario", 85);
    characters.Add("Link", 90);
    characters.Add("Master Chief", 95);
    characters.Add("Kratos", 100);

    Console.WriteLine("Game Characters and Their Power Levels:");
    DisplayDictionary(characters);

    characters["Mario"] = 88; 
    Console.WriteLine("\nAfter Mario's power-up:");
    DisplayDictionary(characters);

    characters.Remove("Master Chief");
    Console.WriteLine("\nAfter Master Chief retires:");
    DisplayDictionary(characters);

    string searchCharacter = "Kratos";
    if (characters.ContainsKey(searchCharacter))
    {
      Console.WriteLine($"\n{searchCharacter} is in the game with a power level of {characters[searchCharacter]}.");
    }
    else
    {
      Console.WriteLine($"\n{searchCharacter} is not in the game.");
    }

    Console.WriteLine($"\nTotal characters in the game: {characters.Count}");
  }
}
