using System;
using System.Collections.Generic;

namespace RecipeProjectMVC.Models.Entities
{
    public partial class Recipe
    {
        public Recipe()
        {
            HealthLabel = new HashSet<HealthLabel>();
            Ingredient = new HashSet<Ingredient>();
            Nutritioninfo = new HashSet<Nutritioninfo>();
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public double? Calories { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
        public double? TotalWeight { get; set; }
        public string Url { get; set; }
        public bool? IsAdded { get; set; }

        public virtual ICollection<HealthLabel> HealthLabel { get; set; }
        public virtual ICollection<Ingredient> Ingredient { get; set; }
        public virtual ICollection<Nutritioninfo> Nutritioninfo { get; set; }
    }
}
