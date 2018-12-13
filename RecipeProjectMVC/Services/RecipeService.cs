using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;

namespace RecipeProjectMVC.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext _context;
        public RecipeService(RecipeDbContext recipeDbContext)
        {
            _context = recipeDbContext;
        }

        public HomeViewModel GetHomeViewModel()
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
            var query = _context.Recipe.ToList();
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
            return model;

        }

        public IQueryable<Recipe> GetRecipesQuery()
        {
            throw new NotImplementedException();
        }

        public Recipe GetRecípe(int id)
        {
            var item = _context.Recipe.Find(id);
            return _context.Recipe.Find(id);
        }
    }
}
