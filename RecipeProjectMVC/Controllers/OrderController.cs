using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.Services;
using RecipeProjectMVC.Services.Repositories;
using RecipeProjectMVC.ViewModels;

namespace RecipeProjectMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRecipeService _service;
        private readonly OrderRepository _orderRepository;
        private readonly OrderDeploymentService _deploymentService;
        private readonly IMemoryCache _cache;
        public OrderController(IRecipeService service, IMemoryCache cache, OrderRepository orderRepository, OrderDeploymentService orderDeploymentService)
        {
            _service = service;
            _orderRepository = orderRepository;
            _deploymentService = orderDeploymentService;
            _cache = cache;
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
        public IActionResult Create(OrderCreateViewModel orderViewModel, int id)
        {
            if (!ModelState.IsValid && ModelState.Any(x => x.Value.AttemptedValue.ToString().ToLower() == "adrian"))
            {
                var alert = new Alert();
                alert.Sighting = DateTime.Now;
                alert.TypeOfIncident = "Adrian Sighted!";
                _orderRepository.RecordAlert(alert);

                _cache.Set("Alert", alert, TimeSpan.FromDays(1));

                return RedirectToAction(nameof(AdrianView));
            }
            if (ModelState.IsValid)
            {
            orderViewModel.ItemId = id;
                _orderRepository.AddOrder(orderViewModel);
                TempData["Message"] = "Your order has been submitted";
                return RedirectToAction(nameof(OrderCompleted));
            }
            return View(orderViewModel);
        }
        [HttpPost]
        [Route("Order/Deploy/{id?}")]
        public IActionResult DeployOrder(int id)
        {
            _deploymentService.DeployOrder(id);
            return Ok();
        }
        public IActionResult OrderCompleted()
        {
            return View();
        }
        public IActionResult AdrianView()
        {
            return View();
        }

    }
}