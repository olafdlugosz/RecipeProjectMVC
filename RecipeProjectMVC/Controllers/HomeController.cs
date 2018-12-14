using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeProjectMVC.Services;

namespace RecipeProjectMVC.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class HomeController : Controller
    {
        private readonly IRecipeService _service;
        public HomeController(IRecipeService service)
        {
            _service = service;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetHomeViewModelAsync();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var recipe = _service.GetRecípe(id);
            if (recipe == null) return NotFound();

            return View(recipe);
        }
    }
}