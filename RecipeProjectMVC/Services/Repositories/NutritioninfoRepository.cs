using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace RecipeProjectMVC.Repositories
{
    public class NutritioninfoRepository
    {
        private readonly RecipeDbContext _context;
        public NutritioninfoRepository(RecipeDbContext context)
        {
            _context = context;
        }
        public async Task<List<Nutritioninfo>> GetTop10VitaminC()
        {
            var vitminCList = await GetTop10("Vitamin C");

            return vitminCList;
        }
        public async Task<List<Nutritioninfo>> GetTop10Protein()
        {
            var proteinList = await GetTop10("Protein");

            return proteinList;
        }
        public async Task<List<Nutritioninfo>> GetTop10Carbs()
        {
            var carbList = await GetTop10("Carbs");

            return carbList;
        }
        public async Task<List<Nutritioninfo>> GetTop10Fat()
        {
            var fatList = await GetTop10("Fat");

            return fatList;
        }
        public async Task<List<Nutritioninfo>> GetTop10Cholesterol()
        {
            var cholesterolList = await GetTop10("Cholesterol");

            return cholesterolList;
        }
        public async Task<List<Nutritioninfo>> GetLowest10Cholesterol()
        {
            var cholesterolList = await GetLowest10("Cholesterol");

            return cholesterolList;
        }
        public async Task<List<Nutritioninfo>> GetLowest10Sodium()
        {
            var sodiumList = await GetLowest10("Sodium");

            return sodiumList;
        }
        public async Task<List<Nutritioninfo>> GetHighest10Sodium()
        {
            var sodiumList = await GetTop10("Sodium");

            return sodiumList;
        }
        private async Task<List<Nutritioninfo>> GetTop10(string Element)
        {
            var elementList = await Task.Run(() => _context.Nutritioninfo
                    .Where(p => p.Label == Element)
                    .OrderByDescending(p => p.Total.Value)
                    .Select(o => new Nutritioninfo
                    { Label = o.Label, Total = o.Total, RecipeId = o.RecipeId })
                    .Take(10)
                    .ToListAsync());

            return elementList;
        }
        private async Task<List<Nutritioninfo>> GetLowest10(string Element)
        {
            var min = _context.Nutritioninfo.Where(p => p.Label == Element).Min(p => p.Total.Value);
            var elementList = await Task.Run(() => _context.Nutritioninfo
                    .Where(p => p.Label == Element)                    
                    .OrderBy(p => p.Total)
                    .Select(o => new Nutritioninfo
                    { Label = o.Label, Total = o.Total, RecipeId = o.RecipeId })
                    .Take(10)
                    .ToListAsync());

            return elementList;
        }



    }
}
