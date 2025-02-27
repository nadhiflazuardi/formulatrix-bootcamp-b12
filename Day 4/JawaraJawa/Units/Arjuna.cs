using interfaces;

namespace units;

public class Arjuna : Unit
{
  public Arjuna() : base("Arjuna", 300, 8, 50, 10, 89, 50)
  {
    Skills.Add(new PanahPasopati());
  }
}

public class PanahPasopati : ISpecialSkill
{
  public string Name => "Panah Pasopati";
  public int ManaCost => 25;

  public void Execute(Unit user, Unit target)
  {
    int damage = user.PhysicalAttack * 2;
    target.TakeDamage(damage);
    user.UseMana(ManaCost);
    Console.WriteLine($"{user.Name} menggunakan {Name}, memberikan {damage} damage kepada {target.Name}!");
  }
}
