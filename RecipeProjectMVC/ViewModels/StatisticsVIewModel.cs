using RecipeProjectMVC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.ViewModels
{
    public class StatisticsViewModel
    {
        public List<RecipeDTO> top10Calories;
        public List<RecipeDTO> top10VitaminC;
        public List<RecipeDTO> top10Protein;
        public List<RecipeDTO> top10Carbs;
        public List<RecipeDTO> top10Fat;
        public List<RecipeDTO> top10Sodium;
        public List<RecipeDTO> top10Cholesterol;
        public List<RecipeDTO> lowest10Cholesterol;
        public List<RecipeDTO> lowest10Sodium;

    }
}
