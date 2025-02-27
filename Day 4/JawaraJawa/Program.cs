using interfaces;
using units;

class Program
{
  static void Main()
  {
    Arjuna arjuna = new();
    Gatotkaca gatotkaca = new();
    Unit currentPlayer = arjuna;
    Unit opponent = gatotkaca;

    while (currentPlayer.CurrentHp > 0 && opponent.CurrentHp > 0)
    {
      Console.WriteLine("\n================================");
      Console.WriteLine($"Giliran {currentPlayer.Name}!");
      Console.WriteLine("================================");
      Console.WriteLine($"{currentPlayer.Name}");
      Console.WriteLine($"HP: {currentPlayer.CurrentHp}\tMP: {currentPlayer.CurrentMana}");
      Console.WriteLine($"{opponent.Name}");
      Console.WriteLine($"HP: {opponent.CurrentHp}\tMP: {opponent.CurrentMana}");

      Random random = new();
      bool isTurnEnded = false;

      while (!isTurnEnded)
      {
        Console.WriteLine("\nPilih aksi:");
        Console.WriteLine("1. Basic Attack");

        List<ISpecialSkill> skills = currentPlayer.GetSkills();
        for (int i = 0; i < skills.Count; i++)
        {
          Console.WriteLine($"{i + 2}. {skills[i].Name} (Mana: {skills[i].ManaCost})");
        }

        string inputChoice = Console.ReadLine() ?? "";

        if (inputChoice == "1")
        {
          int damageDealt = currentPlayer.BasicAttack(opponent);
          if (damageDealt > 0)
          {
            Console.WriteLine($"{currentPlayer} menyerang {opponent} dan memberikan {damageDealt} damage!");
          }

          isTurnEnded = true;
        }
        else
        {
          int skillIndex = Convert.ToInt32(inputChoice) - 2;
          bool skillSuccess = currentPlayer.UseSkill(skillIndex, currentPlayer, opponent);
          isTurnEnded = skillSuccess;

          if (!skillSuccess)
          {
            Console.WriteLine("Coba aksi yang lain!");
          }
        }
      }

      Console.WriteLine($"Giliran {currentPlayer.Name} berakhir.");
      int regeneratedHp = currentPlayer.RegenerateHp(currentPlayer.HpRegen);
      int regeneratedMana = currentPlayer.RegenenateMana(currentPlayer.ManaRegen);
      Console.WriteLine($"{currentPlayer.Name} memulihkan {regeneratedHp} HP dan {regeneratedMana} Mana.");

      (currentPlayer, opponent) = (opponent, currentPlayer);

      Console.WriteLine("\nTekan tombol untuk melanjutkan.");
      Console.ReadKey();
    }

    (currentPlayer, opponent) = (opponent, currentPlayer);
    Console.WriteLine($"{opponent} kehabisan HP! {currentPlayer} menang!");
  }
}