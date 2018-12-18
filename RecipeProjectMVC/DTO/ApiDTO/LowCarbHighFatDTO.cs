using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.DTO.ApiDTO
{
    public class LowCarbHighFatDTO
    {
        public string Label { get; set; }
        public double? Carb { get; set; }
        public double? Fat { get; set; }
    }
}
