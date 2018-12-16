using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeProjectMVC.Services;

namespace RecipeProjectMVC.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IRecipeService _service;
        public StatisticsController(IRecipeService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}