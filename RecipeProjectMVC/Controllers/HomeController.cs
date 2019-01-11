using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Repositories;
using RecipeProjectMVC.Services;

namespace RecipeProjectMVC.Controllers
{
    // [Route("[controller]/[action]/{id?}")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IRecipeService _service;

        private readonly NutritioninfoRepository _repository;

        public HomeController(IRecipeService service, NutritioninfoRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            await _repository.GetTop10RecipeDTO("Fat"); //Pontus special
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
        public async Task<TypeAheadData[]> GetTypeAheadDataAsync([FromRoute]string searchTerm)
        {

            return await _service.GetTypeAheadDataAsync(searchTerm);
        }
        //public async Task<IActionResult> Top10View()
        //{
        //    var model = await _service.GetTop10VitaminCRecipes();
        //    var modelCalories = await _service.GetTop10CalorieRecipes();
        //    return View(model);
        //}
        [HttpGet]
        [Route("Home/GetDetails/{id}")]
        public IActionResult GetDetails(int id)
        {
            var model = _service.GetRecípe(id);
            //  var cholesterolList = Mapper.Map<List<CholesterolDTO>>(model).OrderByDescending(x => x.Total);

            return Json(model);
        }
    }
}