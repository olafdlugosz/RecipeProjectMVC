using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeProjectMVC.Services;
using AutoMapper;
using RecipeProjectMVC.DTO.ApiDTO;

namespace RecipeProjectMVC.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class DietController : Controller
    {
        private readonly IRecipeService _service;
        public DietController(IRecipeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetDietViewModel();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetLCHF()
        {
            var model = await _service.GetLowCarbHighFatRecipes();

            var LCHFmodel = Mapper.Map<List<LowCarbHighFatDTO>>(model);

            return Json(LCHFmodel);
            
        }
        [HttpGet]
        public async Task<IActionResult> GetHighProteinLowCarb()
        {
            var model = await _service.GetHighProteinLowCarb();

            var highProteinLowCarbmodel = Mapper.Map<List<HighProteinLowCarbDTO>>(model);

            return Json(highProteinLowCarbmodel);

        }
    }
}