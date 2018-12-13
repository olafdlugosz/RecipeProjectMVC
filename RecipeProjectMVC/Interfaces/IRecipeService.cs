using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;

namespace RecipeProjectMVC.Services
{
    public interface IRecipeService
    {
        Recipe GetRecípe(int id);
        HomeViewModel GetHomeViewModel();
        IQueryable<Recipe> GetRecipesQuery(); 
    }
}
