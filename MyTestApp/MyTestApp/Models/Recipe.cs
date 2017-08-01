using System;
using System.Collections.Generic;

namespace MyTestApp.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public ICollection<Ingredient> Ingredients { get; set; }

        public ICollection<Step> Steps { get; set; }
    }
}