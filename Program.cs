using Microsoft.OpenApi.Models;
using PizzaStore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "Keep track of your tasks", Version = "v1" });
  });


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
  {
     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
  });

app.MapGet("/", () => "Hello World!");

app.MapGet("/pizzas/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync( id ));

app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());

app.MapPost("/pizzas", async (PizzaDb db,Pizza pizza) => {
  await db.Pizzas.AddAsync( pizza );
  await db.SaveChangesAsync();
  return Results.Created($"/pizza/{pizza.Id}",pizza);
});

app.MapPut("/pizzas/{id}", async (PizzaDb db,Pizza pizzaUpdate, int id) => {
  var pizza = await db.Pizzas.FindAsync( id );
  if (pizza == null) return Results.NotFound();
  pizza.Name = pizzaUpdate.Name;
  pizza.Description = pizzaUpdate.Description;
  await db.SaveChangesAsync();
  return Results.Ok();
});

app.MapDelete("/pizzas/{id}", async( PizzaDb db,int id ) => {
  var pizza = await db.Pizzas.FindAsync( id );
  if (pizza == null) return Results.NotFound();
  db.Pizzas.Remove( pizza );
  await db.SaveChangesAsync();
  return Results.Ok();
}); 

app.Run();
