using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecipeProjectMVC.DTO.ApiDTO;
using RecipeProjectMVC.Services;
using AutoMapper;

namespace RecipeProjectMVC.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IRecipeService _service;
        public StatisticsController(IRecipeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetStatisticsViewModel();
            return View(model);
        }
      [HttpGet]
      [Route("Statistics/api/TopProtein")]
        public async Task<IActionResult> TopProtein()
        {
            var model = await _service.GetTop10Protein();
            var proteinList = Mapper.Map<List<ProteinDTO>>(model).OrderByDescending(x => x.Total);
            
            return Json(proteinList);
        }
        [HttpGet]
        [Route("Statistics/api/TopVitaminC")]
        public async Task<IActionResult> TopVitaminC()
        {
            var model = await _service.GetTop10VitaminCRecipes();
            var vitaminClist = Mapper.Map<List<VitaminCDTO>>(model).OrderByDescending(x => x.Total);

            return Json(vitaminClist);
        }
        [HttpGet]
        [Route("Statistics/api/TopCarb")]
        public async Task<IActionResult> TopCarb()
        {
            var model = await _service.GetTop10Carbs();
            var carbList = Mapper.Map<List<CarbsDTO>>(model).OrderByDescending(x => x.Total);

            return Json(carbList);
        }
        [HttpGet]
        [Route("Statistics/api/TopFat")]
        public async Task<IActionResult> TopFat()
        {
            var model = await _service.GetTop10Fat();
            var fatList = Mapper.Map<List<FatDTO>>(model).OrderByDescending(x => x.Total);

            return Json(fatList);
        }
        [HttpGet]
        [Route("Statistics/api/TopCalories")]
        public async Task<IActionResult> TopCalories()
        {
            var model = await _service.GetTop10CalorieRecipes();
            var calorieList = Mapper.Map<List<CaloriesDTO>>(model).OrderByDescending(x => x.Calories);

            return Json(calorieList);
        }
        [HttpGet]
        [Route("Statistics/api/LowCholesterol")]
        public async Task<IActionResult> LowCholesterol()
        {
            var model = await _service.GetLowest10Cholesterol();
            var cholesterolList = Mapper.Map<List<CholesterolDTO>>(model).OrderBy(x => x.Total);

            return Json(cholesterolList);
        }
        [HttpGet]
        [Route("Statistics/api/LowSodium")]
        public async Task<IActionResult> LowSodium()
        {
            var model = await _service.GetLowest10Sodium();
            var sodiumList = Mapper.Map<List<SodiumDTO>>(model).OrderBy(x => x.Total);

            return Json(sodiumList);
        }
        [HttpGet]
        [Route("Statistics/api/TopCholesterol")]
        public async Task<IActionResult> TopCholesterol()
        {
            var model = await _service.GetTop10Cholesterol();
            var cholesterolList = Mapper.Map<List<CholesterolDTO>>(model).OrderByDescending(x => x.Total);

            return Json(cholesterolList);
        }
    }
}