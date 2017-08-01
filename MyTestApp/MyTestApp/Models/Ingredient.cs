using System;
using System.Collections.Generic;

namespace MyTestApp.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public string Name { get; set;  }
        public int UnitID { get; set; }
        public Unit Unit { get; set; }
        public int Quantity { get; set; } 
        //public bool IsDeleted { get; set;  }
    }
}
