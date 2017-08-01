using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestApp.Models
{
    public class Step
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public int Number { get; set; }
        public string Instructions { get; set; }
    }
}
