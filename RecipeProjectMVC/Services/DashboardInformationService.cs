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

namespace RecipeProjectMVC.Services
{
    public class DashBoardInformationService
    {
        private readonly IRecipeService _recipeService;
        private readonly OrderRepository _orderRepo;
        public DashBoardInformationService(IRecipeService recipeService, OrderRepository orderRepository)
        {
            _recipeService = recipeService;
            _orderRepo = orderRepository;
        }

        public async Task<DashboardInfoVM> GetDashBoardInfoVM()
        {
            var model = new DashboardInfoVM();
            model.Orders = await _orderRepo.GetUnShippedOrders();
            model.ShippedOrders = await _orderRepo.GetShippedOrders();
            model.Top5OrderedRecipes =  _recipeService.GetTop5SoldRecipes();
            return model;
        }


    }
}
