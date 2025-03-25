using ContosoPizza.Models;

namespace ContosoPizza.Services;

public static class PizzaService
{
  static List<Pizza> Pizzas { get; }
  static PizzaService()
  {
    Pizzas = new List<Pizza>
    {
      new Pizza {Id = 1, Name = "Classic Italian", IsGlutenFree = false, Price = 15},
      new Pizza {Id = 2, Name = "Veggie", IsGlutenFree = true, Price = 14}
    };
  }

  public static List<Pizza> GetAll() => Pizzas;

  public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

  public static void Add(Pizza pizza)
  {
    pizza.Id = Pizzas.Count+1;
    Pizzas.Add(pizza);
  }

  public static void Delete(int id)
  {
    var pizza = Get(id);
    if (pizza is null)
      return;

    Pizzas.Remove(pizza);
  }

  public static void Update(Pizza pizza)
  {
    var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
    if (index == -1)
      return;

    Pizzas[index] = pizza;
  }
}