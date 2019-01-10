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
        public IEnumerable<KeyValuePair<string, int>> Top5Customers { get; set; }
        public string Alert { get; set; }
        public DateTime? TimeOfIncident { get; set; }
        public double IncidentsToOrdersPearsonsCorrelation { get; set; }
        public double IncidentsToOrdersSpearmansCorrelation { get; set; }
        public string IncidentPredictionCount { get; set; }
        public string IncidentPredictionMonth { get; set; }


    }

}
