using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using RecipeProjectMVC.Repositories;
using RecipeProjectMVC.Services.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace RecipeProjectMVC.Services
{
    public class DashBoardInformationService
    {
        private readonly IRecipeService _recipeService;
        private readonly OrderRepository _orderRepo;
        private readonly IMemoryCache _cache;
        public DashBoardInformationService(IRecipeService recipeService, OrderRepository orderRepository, IMemoryCache cache)
        {
            _recipeService = recipeService;
            _orderRepo = orderRepository;
            _cache = cache;
        }

        public async Task<DashboardInfoVM> GetDashBoardInfoVM()
        {
            var model = new DashboardInfoVM();
            model.Orders = await _orderRepo.GetUnShippedOrders();
            model.ShippedOrders = await _orderRepo.GetShippedOrders();
            model.Top5OrderedRecipes =  _recipeService.GetTop5SoldRecipes();
            model.Top5Customers = _orderRepo.GetTop5Customers();
            if(_cache.Get("Alert") != null)
            {
                var warning = _cache.Get<Alert>("Alert");
                model.Alert = warning.TypeOfIncident;
                model.TimeOfIncident = warning.Sighting;

            }
            else { model.Alert = "No Issues today"; };
            return model;
        }


    }
}
