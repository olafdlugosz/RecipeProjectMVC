using System;
using System.Collections.Generic;

namespace RecipeProjectMVC.Models.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsShipped { get; set; }
    }
}
