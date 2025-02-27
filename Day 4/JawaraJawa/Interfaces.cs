using units;
namespace interfaces;

interface IAttacker
{
  int BasicAttack(Unit target);
}

interface IAttackable
{
  void TakeDamage(int amount);
}

interface ISkillCaster
{
  int UseSkill();
}

public interface ISpecialSkill
{
  string Name { get; }
  int ManaCost { get; }
  void Execute(Unit user, Unit target);
}