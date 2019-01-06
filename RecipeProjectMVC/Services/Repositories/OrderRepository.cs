using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RecipeProjectMVC.Services.Repositories
{
    public class OrderRepository
    {
        private readonly RecipeDbContext _context;
        public OrderRepository(RecipeDbContext context)
        {
            _context = context;
        }
        public void AddOrder(OrderCreateViewModel orderViewModel)
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
        public async Task<List<Order>> GetUnShippedOrders()
        {
            var orders = await _context.Order.Where(o => o.IsShipped == false).ToListAsync();

            return orders;
        }
        public async Task<List<Order>> GetShippedOrders()
        {
            var orders = await _context.Order.Where(o => o.IsShipped == true).ToListAsync();

            return orders;
        }

        public Order GetOrderById(int id)
        {
            var order = _context.Order.Where(o => o.Id == id).FirstOrDefault();
            
            return order;
        }
        public void ShipOrder(int id)
        {
            var order = GetOrderById(id);
            order.IsShipped = true;
            _context.SaveChanges();
        }
        public IEnumerable<KeyValuePair<int, int>> GetTop5SoldRecipeIds()
        {
            var top5 = _context.Order
                .GroupBy(x => x.ItemId)
                .ToDictionary(y => y.Key, y => y.Count()).OrderByDescending(x => x.Value).Take(5);
            return top5;
        }
        public void RecordAlert(Alert alert)
        {
        
            _context.Alert.Add(alert);
            _context.SaveChanges();
        }

    }
}
