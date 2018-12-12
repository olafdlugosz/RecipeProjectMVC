using System;
using System.Collections.Generic;

namespace RecipeProjectMVC.Models.Entities
{
    public partial class HealthLabel
    {
        public int Id { get; set; }
        public string StringValue { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
