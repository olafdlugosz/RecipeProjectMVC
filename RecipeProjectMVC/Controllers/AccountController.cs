﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeProjectMVC.Services;
using RecipeProjectMVC.ViewModels;

namespace RecipeProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _service;

        public AccountController(AccountService accountService)
        {
            _service = accountService;
        }

        [HttpGet]
        [Route("/2hMjOzkAC0iayI1m6IeDhQ/Login")]
        public IActionResult Login(string returnUrl)
        {
            var model = new AdminLogInViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [Route("/2hMjOzkAC0iayI1m6IeDhQ/Login")]
        public async Task<IActionResult> Login(AdminLogInViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Check if credentials is valid (and set auth cookie)
            if (!await _service.TryLoginAsync(viewModel))
            {
                // Show login error
                ModelState.AddModelError(nameof(AdminLogInViewModel.Username), "Invalid credentials");
                return View(viewModel);
            }

            // Redirect user
            if (string.IsNullOrWhiteSpace(viewModel.ReturnUrl))
            {
                //return RedirectToAction(nameof(HomeController.Index), "Home");
                return Redirect("/2hMjOzkAC0iayI1m6IeDhQ/Dashboard");
            }
            else
                return Redirect(viewModel.ReturnUrl);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _service.LogOut();
            return Redirect("/Home/Index");
        }
    }
}
