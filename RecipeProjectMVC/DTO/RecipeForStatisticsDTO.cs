using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.DTO
{
    public class RecipeForStatisticsDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string ElementLabel { get; set; }
        public double? Total { get; set; }
    }
}
