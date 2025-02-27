using interfaces;

namespace units;

public class Gatotkaca : Unit
{
  public Gatotkaca() : base("Gatotkaca", 450, 15, 40, 5, 50, 60)
  {
    Skills.Add(new OtotKawatTulangBesi());
  }
}

public class OtotKawatTulangBesi : ISpecialSkill
{
  public string Name => "Otot Kawat Tulang Besi";
  public int ManaCost => 30;

  public void Execute(Unit user, Unit target)
  {
    user.UseMana(ManaCost);
    int healedHp = user.RegenerateHp((int)user.MaxHp / 3);
    Console.WriteLine($"{user.Name} mengencangkan otot-ototnya, memulihkan {healedHp} HP!");
  }
}