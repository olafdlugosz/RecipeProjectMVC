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
            var VitminList = await Task.Run(() => _context.Nutritioninfo
                .Where(p => p.Label == "Vitamin C")
                .Select(o => new Nutritioninfo
                { Label = o.Label, Total = o.Total, RecipeId = o.RecipeId })
                .OrderByDescending(p => p.Total).ToListAsync());
            var top10 = new List<Nutritioninfo>();
            for (int i = 0; i < 10; i++)
            {
                top10.Add(VitminList[i]);
            }
            return top10;
        }
        
    }
}
