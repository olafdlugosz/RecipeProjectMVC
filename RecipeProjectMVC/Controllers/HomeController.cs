using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Services;

namespace RecipeProjectMVC.Controllers
{
   // [Route("[controller]/[action]/{id?}")]
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
        [Route("Typeahead/{searchTerm}")]
        public async Task<TypeAheadData[]> GetTypeAheadDataAsync([FromRoute]string searchTerm) {

            return await _service.GetTypeAheadDataAsync(searchTerm);
        }
        public async Task<IActionResult> Top10View()
        {
            var model = await _service.GetTop10VitaminCRecipes();
            var modelCalories = await _service.GetTop10CalorieRecipes();
            return View(model);
        }
        [HttpGet]
       [Route("Home/Details/GetDetails/{id}")]
        public IActionResult GetDetails(int id)
        {
            var model = _service.GetRecípe(id);
            //  var cholesterolList = Mapper.Map<List<CholesterolDTO>>(model).OrderByDescending(x => x.Total);

            return Json(model);
        }
    }
}