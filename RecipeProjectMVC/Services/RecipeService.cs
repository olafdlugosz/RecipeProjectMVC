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
            //Generate list of random Ids
            Random random = new Random();
            var randomIds = new List<int>();
            for (int i = idLow; i <= idHigh; i++)
            {
                randomIds.Add(random.Next(idLow, idHigh));
            }
            //Query the database for random id matches
            var taskList = await Task.Run(()=> _context.Recipe.Where(r => randomIds.Contains(r.Id)).ToListAsync());

            // map 20 random Recipes to the ViewModel
            model.Recipes = taskList;
            return  model;

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
            return  await _context.Recipe
                .Where(o => EF.Functions.Like(o.Label, searchTerm))
                .Select(p => new TypeAheadData { Id = p.Id, Name = p.Label })
                .ToArrayAsync();

        }
    }
}
