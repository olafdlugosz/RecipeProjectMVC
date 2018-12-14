using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace RecipeProjectMVC.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext _context;
        public RecipeService(RecipeDbContext recipeDbContext)
        {
            _context = recipeDbContext;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var model = new HomeViewModel();
            //get lowest and highest Ids from database
            var idLow = _context.Recipe.Min(r => r.Id);
            var idHigh = _context.Recipe.Max(r => r.Id);
            Random random = new Random();
            var randomIds = new List<int>();
            for (int i = idLow; i <= idHigh; i++)
            {
                randomIds.Add(i);
            }
            //Get all Recipes from database.. I should cache this later.
            var query =  await Task.Run(() => _context.Recipe.ToList());
            var listToAdd = new List<Recipe>();
            for (int i = 0; i < 20; i++)
            {
                var randomId = randomIds[random.Next(0, randomIds.Count)];
                
                var item = query.Single(r => r.Id == randomId);
          
                listToAdd.Add(item);
                randomIds.Remove(randomId);

            }
            // map 20 random Recipes to the ViewModel
            model.Recipes = listToAdd;
            return  model;

        }

        public IQueryable<Recipe> GetRecipesQuery()
        {
            throw new NotImplementedException();
        }

        public RecipeDTO GetRecípe(int id)
        {
            //TODO FIX THIS FUCKING QUERY
            var recipeEntity = _context.Recipe
                .Include(o => o.Ingredient)
                .Include(g => g.HealthLabel)
                .Include(z => z.Nutritioninfo)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            var recipe = Mapper.Map<RecipeDTO>(recipeEntity);
            //recipe.HealthLabel = Mapper.Map<ICollection<HealthLabelDTO>>(source: recipeEntity.HealthLabel.ToList());
            //recipe.Ingredient = Mapper.Map<ICollection<IngredientDTO>>(source: recipeEntity.Ingredient.ToList());
            //recipe.Nutritioninfo = Mapper.Map<ICollection<NutritioninfoDTO>>(source: recipeEntity.Nutritioninfo.ToList());
            return recipe;
        }
    }
}
