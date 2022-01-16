namespace PizzaStore.DB;

public record Pizza 
{
  public int Id { get; set; }
  public string ? Name { get; set; }
}

public class PizzaDB 
{
  private static List<Pizza> _pizzas = new List<Pizza>()
  {
    new Pizza { Id=1, Name = "Peperonim pizza de peperoni con algunos condimentos mas"},
    new Pizza { Id=2, Name = "Hawaiana, pizza con queso y entre otros condimentos"},
    new Pizza { Id=3, Name = "Rasqued, pizza regular "}
  };

  public static List<Pizza> GetPizzas()
  {
   return _pizzas; 
  }

  //One Pizza
  public static Pizza? GetOnePizza(int id)
  {
    return _pizzas.SingleOrDefault( pizza => pizza.Id == id);
  }
  //Create Pizza

  public static Pizza createPizza (Pizza pizza)
  {
    _pizzas.Add(pizza);
    return pizza;
  }
  //Update Pizzza
  public static Pizza updatePizza (Pizza pizzaUpdate )
  {
    _pizzas = _pizzas.Select( pizza => {
      if (pizza.Id == pizzaUpdate.Id){
        pizza.Name = pizzaUpdate.Name;
      }
      return pizza;
    }).ToList();

    return pizzaUpdate;
  }

  public static void deletePizza ( int id )
  {
    _pizzas.RemoveAll(pizza => pizza.Id == id);
  }
}