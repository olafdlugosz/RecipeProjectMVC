using System;
using System.Collections.Generic;

namespace RecipeProjectMVC.Models.Entities
{
    public partial class Nutritioninfo
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public double? Total { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
