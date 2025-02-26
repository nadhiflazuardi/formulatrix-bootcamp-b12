class Character
{
  public string Name { get; private set; }
  public int Health { get; private set; }
  public int PhysicalAttack { get; set; }
  public int Regen { get; set; }

  public Character(string name, int health, int physicalAttack, int regen)
  {
    Name = name;
    Health = health;
    PhysicalAttack = physicalAttack;
    Regen = regen;
  }

  public void TakeDamage(int amount)
  {
    Health = Math.Max(Health - amount, 0); // Reduce health but prevent negative
    if (Health > 0)
    {
      Console.WriteLine($"{Name} took {amount} damage. Health left: {Health}");
    }
    else
    {
      Console.WriteLine($"{Name} ran out of health.");
    }
  }

  public void Heal(int amount)
  {
    Health += amount;
    Console.WriteLine($"{Name} healed {amount} of health. Current health: {Health}");
  }
}

interface IAttack
{
  void Attack(Character target);
}

interface IHeal
{
  void HealSelf();
}


class Warrior : Character, IAttack
{
  public Warrior() : base("Warrior", 100, 20, 5) // Default Warrior stats
  {

  }

  public void Attack(Character target)
  {
    Console.WriteLine($"{Name} attacks {target.Name} for {PhysicalAttack} damage!");
    target.TakeDamage(PhysicalAttack);
  }

  public void Rampage(Character target)
  {
    Random random = new();
    bool isRampageActive = random.Next(2) == 1;

    if (isRampageActive)
    {
      Console.WriteLine($"{Name} enters a Rampage! Damage triples!");
      target.TakeDamage(PhysicalAttack * 3);
    }
    else
    {
      Console.WriteLine($"{Name} tried Rampage but failed!");
    }
  }
}

class Mage : Character, IAttack, IHeal
{
  public Mage() : base("Mage", 100, 10, 15)
  {

  }

  public void Attack(Character target)
  {
    Console.WriteLine($"{Name} casts a spell on {target.Name} for {PhysicalAttack} damage!");
    target.TakeDamage(PhysicalAttack);
  }

  public void HealSelf()
  {
    Console.WriteLine($"{Name} heals themselves for {Regen} health!");
    Heal(Regen);
  }

  public void MegaHeal()
  {
    Random random = new();
    bool isMegaHealActive = random.Next(2) == 1;
    int originalHeal = Regen;

    if (isMegaHealActive)
    {
      Console.WriteLine($"{Name} activated Mega Heal! Healing factor triples!");
      Regen *= 3;
    }

    Heal(Regen); // Heals itself

    Regen = originalHeal;
  }
}

class Program
{
  static void Main()
  {
    Warrior warrior = new Warrior();
    Mage mage = new Mage();
    Character currentPlayer = warrior;
    Character opponent = mage;

    Console.WriteLine("🔥 Welcome to the Turn-Based Battle Game! 🔥");
    Console.WriteLine($"{warrior.Name} vs {mage.Name}");
    Console.WriteLine("Let the battle begin!\n");

    // 🎮 Turn-Based Game Loop
    while (warrior.Health > 0 && mage.Health > 0)
    {
      Console.WriteLine($"{currentPlayer.Name}'s Turn! Choose an action:");
      Console.WriteLine("1. Attack");
      if (currentPlayer is Warrior) Console.WriteLine("2. Rampage");
      if (currentPlayer is Mage)
      {
        Console.WriteLine("2. Heal");
        Console.WriteLine("3. Mega Heal");
      }
      Console.Write("Enter your choice: ");

      string choice = Console.ReadLine();
      Console.Clear(); // Clears console for a cleaner look

      if (choice == "1")
      {
        if (currentPlayer is IAttack attacker)
        {
          attacker.Attack(opponent);
        }
      }
      else if (choice == "2")
      {
        if (currentPlayer is Warrior warriorPlayer)
        {
          warriorPlayer.Rampage(opponent);
        }
        else if (currentPlayer is Mage magePlayer)
        {
          magePlayer.HealSelf();
        }
      }
      else if (choice == "3")
      {
        if (currentPlayer is Mage magePlayer)
        {
          magePlayer.MegaHeal();
        }
      }
      else
      {
        Console.WriteLine("Invalid choice! Turn skipped.");
      }

      // ⚰ Check if Opponent Died
      if (opponent.Health <= 0)
      {
        Console.WriteLine($"\n🏆 {currentPlayer.Name} WINS THE BATTLE! 🏆");
        break;
      }

      // 🔄 Switch Turns
      (currentPlayer, opponent) = (opponent, currentPlayer);
    }

    Console.WriteLine("\nGame Over! Thanks for playing. 🎮");
  }
}
