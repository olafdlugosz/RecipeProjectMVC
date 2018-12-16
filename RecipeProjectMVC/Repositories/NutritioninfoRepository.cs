using RecipeProjectMVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.Repositories
{
    public class NutritioninfoRepository
    {
        private readonly RecipeDbContext _context;
        public NutritioninfoRepository(RecipeDbContext context)
        {
            _context = context;
        }
        
    }
}
