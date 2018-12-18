using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public double? Calories { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
        public double? TotalWeight { get; set; }
        public string Url { get; set; }
        public bool? IsAdded { get; set; }

        public virtual ICollection<HealthLabelDTO> HealthLabel { get; set; }
        public virtual ICollection<IngredientDTO> Ingredient { get; set; }
        public virtual ICollection<NutritioninfoDTO> Nutritioninfo { get; set; }

    }
}
