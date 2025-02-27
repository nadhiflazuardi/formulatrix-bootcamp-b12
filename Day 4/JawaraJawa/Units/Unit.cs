using interfaces;
namespace units;

public class Unit : IAttackable, IAttacker
{
  public string Name { get; private set; }
  public int MaxHp { get; private set; }
  public int CurrentHp { get; private set; }
  public int HpRegen { get; private set; }
  public int MaxMana { get; private set; }
  public int CurrentMana { get; private set; }
  public int ManaRegen { get; private set; }
  public int Agility { get; private set; }
  public int PhysicalAttack { get; private set; }
  protected List<ISpecialSkill> Skills { get; set; } = new List<ISpecialSkill>();
  public Random random = new();

  public Unit(string name, int maxHp, int hpRegen, int maxMana, int manaRegen, int agility, int physicalAttack)
  {
    Name = name;
    MaxHp = maxHp;
    CurrentHp = maxHp;
    HpRegen = hpRegen;
    MaxMana = maxMana;
    CurrentMana = maxMana;
    ManaRegen = manaRegen;
    Agility = agility;
    PhysicalAttack = physicalAttack;
  }

  public int BasicAttack(Unit target)
  {
    if (target.Dodge())
    {
      Console.WriteLine($"{Name} menyerang, tetapi {target.Name} berhasil menghindari serangan!");
      return 0;
    }

    double rng = random.NextDouble();
    int damage = Convert.ToInt32(PhysicalAttack * rng);
    target.TakeDamage(damage);

    return damage;
  }

  public void TakeDamage(int amount)
  {
    CurrentHp -= amount;
  }

  public void UseMana(int amount)
  {
    CurrentMana -= amount;
  }

  public List<ISpecialSkill> GetSkills()
  {
    return Skills;
  }

  public bool UseSkill(int skillIndex, Unit user, Unit target)
  {
    if (skillIndex < 0 || skillIndex >= Skills.Count)
    {
      return false;
    }

    ISpecialSkill skill = Skills[skillIndex];

    if (CurrentMana < skill.ManaCost)
    {
      Console.WriteLine($"Mana {Name} tidak cukup untuk menggunakan {skill.Name}!");
      return false;
    }

    skill.Execute(user, target);
    return true;
  }

  public bool Dodge()
  {
    double rng = random.NextDouble();

    return rng < (double)Agility / 100;
  }

  public int RegenerateHp(int amount)
  {
    CurrentHp += amount;

    return amount;
  }

  public int RegenenateMana(int amount)
  {
    CurrentMana += amount;

    return amount;
  }
}