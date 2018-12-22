using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeProjectMVC.Services;
using RecipeProjectMVC.Services.Repositories;
using RecipeProjectMVC.ViewModels;

namespace RecipeProjectMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRecipeService _service;
        private readonly OrderRepository _orderRepository;
        public OrderController(IRecipeService service, OrderRepository orderRepository)
        {
            _service = service;
            _orderRepository = orderRepository;
        }
        [HttpGet]
        [Route("Order/Create/{id}")]
        public IActionResult Create(int id)
        {
            var ID = id;
            return View();
          
        }
        [HttpPost]
        [Route("Order/Create/{id?}")]
        public IActionResult Create(OrderViewModel orderViewModel, int id)
        {
            if (ModelState.IsValid)
            {
            orderViewModel.ItemId = id;
                _orderRepository.AddOrder(orderViewModel);
                TempData["Message"] = "Your order has been submitted";
                return RedirectToAction(nameof(OrderCompleted));
            }
            return View(orderViewModel);
        }
        public IActionResult OrderCompleted()
        {
            return View();
        }
    }
}