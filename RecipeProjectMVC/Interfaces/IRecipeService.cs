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
        Task<TypeAheadData[]> GetTypeAheadDataAsync(string searchTerm);
        Task<List<RecipeDTO>> GetTop10VitaminCRecipes();
        Task<List<RecipeDTO>> GetTop10CalorieRecipes();
        Task<List<RecipeDTO>> GetTop10Protein();
        Task<List<RecipeDTO>> GetTop10Carbs();
        Task<List<RecipeDTO>> GetTop10Fat();
        Task<List<RecipeDTO>> GetLowest10Cholesterol();
        Task<List<RecipeDTO>> GetLowest10Sodium();
        Task<List<RecipeDTO>> GetTop10Cholesterol();
        Task<StatisticsViewModel> GetStatisticsViewModel();
        Task<List<RecipeDTO>> GetLowCarbHighFatRecipes();
        Task<List<RecipeDTO>> GetHighProteinLowCarb();
        Task<DietViewModel> GetDietViewModel();




    }
}
