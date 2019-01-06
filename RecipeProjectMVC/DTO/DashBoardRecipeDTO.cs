using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.DTO
{
    public class DashboardRecipeDTO
    {
        public int RecipeId { get; set; }
        public string Label { get; set; }
        public int Count { get; set; }
    }
}
