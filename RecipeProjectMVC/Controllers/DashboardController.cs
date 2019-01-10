using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecipeProjectMVC.Services;
using RecipeProjectMVC.Services.Repositories;

namespace RecipeProjectMVC.Controllers
{
   // [Authorize] 
    public class DashboardController : Controller
    {
        private readonly OrderRepository _orderRepo;
        private readonly OrderDeploymentService _deploymentService;
        private readonly DashBoardInformationService _dashboardInfoService;
        private readonly IMemoryCache _cache;
        public DashboardController(OrderRepository orderRepository,
            OrderDeploymentService deploymentService,
            DashBoardInformationService dashBoardInformationService,
            IMemoryCache cache)
        {
            _orderRepo = orderRepository;
            _deploymentService = deploymentService;
            _dashboardInfoService = dashBoardInformationService;
            _cache = cache;
        }
        [Authorize]
        [Route("/2hMjOzkAC0iayI1m6IeDhQ/Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
           
            var model = await _dashboardInfoService.GetDashBoardInfoVM();
            return View(model);
        }
       // [HttpPost]
       [Authorize]
        [Route("DashBoard/Order/Deploy/{id?}")]
        public IActionResult DeployOrder(int id)
        {
            _deploymentService.DeployOrder(id);
            return Ok();
        }
    }
}