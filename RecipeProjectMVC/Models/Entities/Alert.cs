using System;
using System.Collections.Generic;

namespace RecipeProjectMVC.Models.Entities
{
    public partial class Alert
    {
        public int Id { get; set; }
        public DateTime Sighting { get; set; }
        public string TypeOfIncident { get; set; }
    }
}
