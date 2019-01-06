using RecipeProjectMVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Order> ShippedOrders { get; set; }
    }
}
