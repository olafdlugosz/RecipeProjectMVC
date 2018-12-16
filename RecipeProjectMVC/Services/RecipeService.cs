using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using RecipeProjectMVC.Repositories;

namespace RecipeProjectMVC.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext _context;
        private readonly NutritioninfoRepository _nutritionInfoRepo;
        public RecipeService(RecipeDbContext recipeDbContext, NutritioninfoRepository repository)
        {
            _context = recipeDbContext;
            _nutritionInfoRepo = repository;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var model = new HomeViewModel();
            //get lowest and highest Ids from database
            var idLow = _context.Recipe.Min(r => r.Id);
            var idHigh = _context.Recipe.Max(r => r.Id);
            //Generate list of random Ids
            Random random = new Random();
            var randomIds = new List<int>();
            for (int i = idLow; i <= idHigh; i++)
            {
                randomIds.Add(random.Next(idLow, idHigh));
            }
            //Query the database for random id matches
            var taskList = await Task.Run(() => _context.Recipe.Where(r => randomIds.Contains(r.Id)).ToListAsync());

            // map 20 random Recipes to the ViewModel
            model.Recipes = taskList;
            return model;

        }

        public IQueryable<Recipe> GetRecipesQuery()
        {
            throw new NotImplementedException();
        }

        public RecipeDTO GetRecípe(int id)
        {
            var recipeEntity = _context.Recipe
                .Include(o => o.Ingredient)
                .Include(g => g.HealthLabel)
                .Include(z => z.Nutritioninfo)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            var recipe = Mapper.Map<RecipeDTO>(recipeEntity);
            return recipe;
        }

        public async Task<TypeAheadData[]> GetTypeAheadDataAsync(string searchTerm)
        {
            return await _context.Recipe
                .Where(o => EF.Functions.Like(o.Label, searchTerm))
                .Select(p => new TypeAheadData { Id = p.Id, Name = p.Label })
                .ToArrayAsync();

        }
        public async Task<List<RecipeDTO>> GetTop10VitaminCRecipes()
        {
            var top10VitaminCList = await _nutritionInfoRepo.GetTop10VitaminC();
            var foreignKeysToMatch = new List<int>();
            foreach (var item in top10VitaminCList)
            {
                foreignKeysToMatch.Add(item.RecipeId);
            }
            var top10VitaminCRecipes = await Task.Run(() => _context.Recipe
                .Include(o => o.Ingredient)
                .Include(g => g.HealthLabel)
                .Include(z => z.Nutritioninfo)
                .Where(p => foreignKeysToMatch.Contains(p.Id)).ToListAsync());

            var finalTop10 = Mapper.Map<List<RecipeDTO>>(top10VitaminCRecipes);

            return finalTop10;
        }
    }
}
