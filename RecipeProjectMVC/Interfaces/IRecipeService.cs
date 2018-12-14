using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;

namespace RecipeProjectMVC.Services
{
    public interface IRecipeService
    {
        RecipeDTO GetRecípe(int id);
        Task<HomeViewModel> GetHomeViewModelAsync();
        IQueryable<Recipe> GetRecipesQuery();
    }
}
