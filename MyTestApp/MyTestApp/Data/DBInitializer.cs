using MyTestApp.Models;
using System;
using System.Linq;

namespace MyTestApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TestContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Recipes.Any())
            {
                return;   // DB has been seeded
            }

            var units = new Unit[]
            {
            new Unit{UnitName="tsp"},
            new Unit{UnitName="cup"},
            new Unit{UnitName="pinch"},
            new Unit{UnitName="oz"}
            };
            foreach (Unit s in units)
            {
                context.Units.Add(s);
            }
            context.SaveChanges();

            Recipe r = new Recipe { Name = "Recipe 1" };
            context.Recipes.Add(r);
            context.SaveChanges();
            r = context.Recipes.FirstOrDefault(recipe => recipe.Name == "Recipe 1");
            if (r != null)
            {
                context.Steps.Add(new Step { RecipeID = r.ID, Number = 1, Instructions = "Do step 1" });
                context.Steps.Add(new Step { RecipeID = r.ID, Number = 2, Instructions = "Do step 2" });
                context.Ingredients.Add(new Ingredient { RecipeID = r.ID, Name = "item 1", Quantity = 1, UnitID = 1 });
                context.Ingredients.Add(new Ingredient { RecipeID = r.ID, Name = "item 2", Quantity = 1, UnitID = 2 });
                context.Ingredients.Add(new Ingredient { RecipeID = r.ID, Name = "item 3", Quantity = 1, UnitID = 3 });
            }
            context.SaveChanges();
        }
    }
}