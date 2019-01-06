using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.ViewModels
{
    public class DashboardInfoVM
    {
        public List<Order> Orders { get; set; }
        public List<Order> ShippedOrders { get; set; }
        public List<DashboardRecipeDTO> Top5OrderedRecipes { get; set; }
        public string Alert { get; set; }
        public DateTime? TimeOfIncident { get; set; }


    }

}
