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
        //TODO: There's a bug. list contains 20 ids but taskList returns only 15 items.
        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var model = new HomeViewModel();
            //get lowest and highest Ids from database
            var idLow = _context.Recipe.Min(r => r.Id);
            var idHigh = _context.Recipe.Max(r => r.Id);
           // Generate list of random Ids
            Random random = new Random();
            var randomIds = new List<int>();
            while (randomIds.Count < 20)
            {

                var randomid = random.Next(idLow, idHigh);
                if (!randomIds.Contains(randomid))
                {
                    randomIds.Add(randomid);
                }
           
            }
            
            //Query the database for random id matches
            var taskList = await _context.Recipe.Where(r => randomIds.Contains(r.Id)).ToListAsync();

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
            //Get the top10 Vitamin C values
            var top10VitaminCList = await _nutritionInfoRepo.GetTop10VitaminC();
            return await MatchForeignKeysWithSourceObjects(top10VitaminCList);
        }
        public async Task<List<RecipeDTO>> GetTop10Protein()
        {
            //Get the top10 values
            var top10ProteinList = await _nutritionInfoRepo.GetTop10Protein();
            return await MatchForeignKeysWithSourceObjects(top10ProteinList);
        }
        public async Task<List<RecipeDTO>> GetTop10Carbs()
        {
            //Get the top10 values
            var top10CarbList = await _nutritionInfoRepo.GetTop10Carbs();
            return await MatchForeignKeysWithSourceObjects(top10CarbList);
        }
        public async Task<List<RecipeDTO>> GetTop10Fat()
        {
            //Get the top10 values
            var top10FatList = await _nutritionInfoRepo.GetTop10Fat();
            return await MatchForeignKeysWithSourceObjects(top10FatList);
        }
        public async Task<List<RecipeDTO>> GetTop30Fat()
        {
            //Get the top10 values
            var top10FatList = await _nutritionInfoRepo.GetTop30Fat();
            return await MatchForeignKeysWithSourceObjects(top10FatList);
        }
        public async Task<List<RecipeDTO>> GetTop10Cholesterol()
        {
            //Get the top10 values
            var top10CholesterolList = await _nutritionInfoRepo.GetTop10Cholesterol();
            return await MatchForeignKeysWithSourceObjects(top10CholesterolList);
        }
        public async Task<List<RecipeDTO>> GetLowest10Cholesterol()
        {
            //Get the lowest10 values
            var lowest10CholesterolList = await _nutritionInfoRepo.GetLowest10Cholesterol();
            return await MatchForeignKeysWithSourceObjects(lowest10CholesterolList);
        }
        public async Task<List<RecipeDTO>> GetLowest10Sodium()
        {
            //Get the top10 values
            var lowest10SodiumList = await _nutritionInfoRepo.GetLowest10Sodium();
            return await MatchForeignKeysWithSourceObjects(lowest10SodiumList);
        }
        public async Task<List<RecipeDTO>> GetTop10Sodium()
        {
            //Get the top10 values
            var top10SodiumList = await _nutritionInfoRepo.GetHighest10Sodium();
            return await MatchForeignKeysWithSourceObjects(top10SodiumList);
        }
        public async Task<List<RecipeDTO>> GetLowestCarbs()
        {
            //Get the top10 values
            var lowestCarbList = await _nutritionInfoRepo.GetLowest10Carbs();
            return await MatchForeignKeysWithSourceObjects(lowestCarbList);
        }
        public async Task<List<RecipeDTO>> GetLowest30Carbs()
        {
            //Get the top10 values
            var lowestCarbList = await _nutritionInfoRepo.GetLowest30Carbs();
            return await MatchForeignKeysWithSourceObjects(lowestCarbList);
        }

        private async Task<List<RecipeDTO>> MatchForeignKeysWithSourceObjects(List<Nutritioninfo> elementInfo)
        {
            //Create list of foreign keys
            var foreignKeysToMatch = new List<int>();
            foreach (var item in elementInfo)
            {
                foreignKeysToMatch.Add(item.RecipeId);
            }
            //Get the top 10 Recipe Objects from the database
            var elements = await _context.Recipe
                .Include(o => o.Ingredient)
                .Include(g => g.HealthLabel)
                .Include(z => z.Nutritioninfo)
                .Where(p => foreignKeysToMatch.Contains(p.Id)).ToListAsync();

            //s var orderedElement = elements.Where
            //map to DTO
            var final10 = Mapper.Map<List<RecipeDTO>>(elements);

            return final10;
        }

        //private async Task<List<RecipeForStatisticsDTO>> MatchForeignKeysWithSourceObjectsForStatistics(List<Nutritioninfo> elementInfo)
        //{
        //    //Create list of foreign keys
        //    var foreignKeysToMatch = new List<int>();
        //    foreach (var item in elementInfo)
        //    {
        //        foreignKeysToMatch.Add(item.RecipeId);
        //    }
        //    //Get the top 10 Recipe Objects from the database
        //    var elements = await _context.Recipe
        //        .Include(o => o.Ingredient)
        //        .Include(g => g.HealthLabel)
        //        .Include(z => z.Nutritioninfo)
        //        .Where(p => foreignKeysToMatch.Contains(p.Id)).ToListAsync();
        //    //map to DTO
        //    var final10 = Mapper.Map<List<RecipeForStatisticsDTO>>(elements);

        //    return final10;
        //}
        public async Task<List<RecipeDTO>> GetTop10CalorieRecipes()
        {
            var topTenCalories = await Task.Run(() => _context.Recipe
                .Include(o => o.Ingredient)
                .Include(g => g.HealthLabel)
                .Include(z => z.Nutritioninfo)
                .OrderByDescending(p => p.Calories)
                .Take(10)
                .ToListAsync());

            var topTenDTO = Mapper.Map<List<RecipeDTO>>(topTenCalories);

            return topTenDTO;

        }

        public async Task<StatisticsViewModel> GetStatisticsViewModel()
        {
            var model = new StatisticsViewModel();
            model.lowest10Cholesterol = await GetLowest10Cholesterol();
            model.lowest10Sodium = await GetLowest10Sodium();
            model.top10Calories = await GetTop10CalorieRecipes();
            model.top10Carbs = await GetTop10Carbs();
            model.top10Fat = await GetTop10Fat();
            model.top10Protein = await GetTop10Protein();
            model.top10Cholesterol = await GetTop10Cholesterol();
            model.top10VitaminC = await GetTop10VitaminCRecipes();
            model.top10Sodium = await GetTop10Sodium();

            return model;

        }
        public async Task<DietViewModel> GetDietViewModel()
        {
            var model = new DietViewModel();
            model.LCHFRecipes = await GetLowCarbHighFatRecipes();
            model.HighProteinLowCarbRecipes = await GetHighProteinLowCarb();

            return model;
        }
       
        public async Task<List<RecipeDTO>> GetLowCarbHighFatRecipes()
        {

            var lowCarbHighFatRecipeIds = await _nutritionInfoRepo.GetLowCarbHighFat();
            return await MatchForeignKeysWithSourceObjects(lowCarbHighFatRecipeIds);

        }
        public async Task<List<RecipeDTO>> GetHighProteinLowCarb()
        {
            var model = await _nutritionInfoRepo.GetHighProteinLowCarb();

            return await MatchForeignKeysWithSourceObjects(model);
        }
    }
}
