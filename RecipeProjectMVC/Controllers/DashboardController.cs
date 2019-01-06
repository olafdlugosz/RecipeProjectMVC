using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeProjectMVC.Services;
using RecipeProjectMVC.Services.Repositories;

namespace RecipeProjectMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly OrderRepository _orderRepo;
        private readonly OrderDeploymentService _deploymentService;
        public DashboardController(OrderRepository orderRepository, OrderDeploymentService deploymentService)
        {
            _orderRepo = orderRepository;
            _deploymentService = deploymentService;
        }
        public async Task<IActionResult> Dashboard()
        {
            var model = await _orderRepo.GetOrderViewModel();
            return View(model);
        }
       // [HttpPost]
        [Route("DashBoard/Order/Deploy/{id?}")]
        public IActionResult DeployOrder(int id)
        {
            _deploymentService.DeployOrder(id);
            return Ok();
        }
    }
}