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
        public async Task<OrderViewModel> GetOrderViewModel()
        {
            var model = new OrderViewModel();
            model.Orders = await GetUnShippedOrders();
            model.ShippedOrders = await GetShippedOrders();
            return model;
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

    }
}
