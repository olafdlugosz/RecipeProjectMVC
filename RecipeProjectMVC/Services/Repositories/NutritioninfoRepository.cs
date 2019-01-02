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
        public async Task<List<Nutritioninfo>> GetTop30Fat()
        {
            var fatList = await GetTop30("Fat");

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
        public async Task<List<Nutritioninfo>> GetLowest10Carbs()
        {
            var carbsList = await GetLowest10("Carbs");

            return carbsList;
        }
        public async Task<List<Nutritioninfo>> GetLowest30Carbs()
        {
            var carbsList = await GetLowest30("Carbs");

            return carbsList;
        }
        private async Task<List<Nutritioninfo>> GetTop10(string Element)
        {
            var elementList = await _context.Nutritioninfo.TagWith("This is Olafs special query!")
                    .Where(p => p.Label == Element)
                    .Select(o => new Nutritioninfo
                    { Label = o.Label, Total = o.Total, RecipeId = o.RecipeId })
                    .OrderByDescending(p => p.Total.Value)
                    .Take(10)
                    .ToListAsync();

            return elementList;
        }
        private async Task<List<Nutritioninfo>> GetLowest10(string Element)
        {
            //var min = _context.Nutritioninfo.Where(p => p.Label == Element).Min(p => p.Total.Value);
            var elementList = await _context.Nutritioninfo
                    .Where(p => p.Label == Element)
                    .OrderBy(p => p.Total)
                    .Select(o => new Nutritioninfo
                    { Label = o.Label, Total = o.Total, RecipeId = o.RecipeId })
                    .Take(10)
                    .ToListAsync();

            return elementList;
        }
        private async Task<List<Nutritioninfo>> GetTop30(string Element)
        {
            var elementList = await Task.Run(() => _context.Nutritioninfo
                    .Where(p => p.Label == Element)
                    .OrderByDescending(p => p.Total.Value)
                    .Select(o => new Nutritioninfo
                    { Label = o.Label, Total = o.Total, RecipeId = o.RecipeId })
                    .Take(30)
                    .ToListAsync());

            return elementList;
        }
        private async Task<List<Nutritioninfo>> GetLowest30(string Element)
        {
            //TODO: ask Oscar about the Task.Run();
            var elementList = await Task.Run(() => _context.Nutritioninfo
                    .Where(p => p.Label == Element)
                    .OrderBy(p => p.Total)
                    .Select(o => new Nutritioninfo
                    { Label = o.Label, Total = o.Total, RecipeId = o.RecipeId })
                    .Take(30)
                    .ToListAsync());

            return elementList;
        }
        public async Task<List<Nutritioninfo>> GetHighProteinLowCarb()
        {
            var highProtein = await GetTop10Protein();
            var lowCarb = await GetLowest30Carbs();
            var highProteinLowCarbRecipeIds = highProtein.Intersect(lowCarb, new NutritionInfoRecipeIdComparer()).ToList();

            return highProteinLowCarbRecipeIds;
        }
        //PONTUS SPECIAL!
        public async Task<List<RecipeDTO>> GetTop10RecipeDTO(string Element)
        {
            var elementList = _context.Recipe.TagWith("This is Pontus special query!")
                    .Where(r => r.Nutritioninfo.Select(n => n.Label).Contains(Element))
                    .Select(o => new RecipeDTO
                    {
                        Label = o.Label,
                        Calories = o.Calories,
                        Id = o.Id,
                        Image = o.Image,
                        IsAdded = o.IsAdded,
                        Source = o.Source,
                        Url = o.Url,
                        TotalWeight = o.TotalWeight,
                        HealthLabel = o.HealthLabel.Select(h => new HealthLabelDTO
                        {
                            StringValue = h.StringValue
                        }).ToList(),
                        Ingredient = o.Ingredient.Select(i => new IngredientDTO
                        {
                            Text = i.Text,
                            Weight = i.Weight
                        }).ToList(),
                        Nutritioninfo = o.Nutritioninfo.Select(n => new NutritioninfoDTO
                        {
                            Label = n.Label,
                            Total = n.Total
                        }).ToList()
                    })
                    .OrderByDescending(p => p.Nutritioninfo.Single(x => x.Label == Element).Total)
                    .Take(10)
                    .ToList();

            await Task.Delay(0);
            return elementList;
        }


        public async Task<List<Nutritioninfo>> GetLowCarbHighFat()
        {
            var lowCarb = await GetLowest30Carbs();
            var highFat = await GetTop10Fat();

            var LowCarbHighFatRecipeIds = highFat.Intersect(lowCarb, new NutritionInfoRecipeIdComparer()).ToList();

            return LowCarbHighFatRecipeIds;
        }
        //Custom Comparison Class for the Queries using Interect.
        private class NutritionInfoRecipeIdComparer : IEqualityComparer<Nutritioninfo>
        {
            public bool Equals(Nutritioninfo x, Nutritioninfo y)
            {
                if (x.RecipeId == y.RecipeId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Nutritioninfo obj)
            {
                return obj.RecipeId.GetHashCode();
            }
        }

    }
}
