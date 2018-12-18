using RecipeProjectMVC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.ViewModels
{
    public class DietViewModel
    {
        public List<RecipeDTO> LCHFRecipes;
        public List<RecipeDTO> HighProteinLowCarbRecipes;
    }
}
