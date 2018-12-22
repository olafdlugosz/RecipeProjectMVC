using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.Services.Repositories
{
    public class OrderRepository
    {
        private readonly RecipeDbContext _context;
        public OrderRepository(RecipeDbContext context)
        {
            _context = context;
        }
        public void AddOrder(OrderViewModel orderViewModel)
        {
            var order = new Order
            {
                CreationDate = DateTime.Now,
                CustomerName = orderViewModel.CustomerName,
                CustomerEmail = orderViewModel.CustomerEmail,
                ItemId = orderViewModel.ItemId,
                IsShipped = false
            };

            _context.Order.Add(order);
            _context.SaveChanges();

        }

    }
}
