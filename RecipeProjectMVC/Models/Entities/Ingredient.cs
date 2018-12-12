using System;
using System.Collections.Generic;

namespace RecipeProjectMVC.Models.Entities
{
    public partial class Ingredient
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public double? Weight { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
