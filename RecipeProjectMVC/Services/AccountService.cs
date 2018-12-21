using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RecipeProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.Services
{
    public class AccountService
    {
        private readonly IdentityDbContext _identityContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(IdentityDbContext identityContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _identityContext = identityContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> TryLoginAsync(AdminLogInViewModel viewModel)
        {
            // Create DB schema (first time)
            //var createSchemaResult = await _identityContext.Database.EnsureCreatedAsync();

            // Create a hard coded user (first time)
           // var createResult = await _userManager.CreateAsync(new IdentityUser("Admin"), "Password_123");

            var loginResult = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            return loginResult.Succeeded;
        }
        public async Task LogOut()
        {
             await _signInManager.SignOutAsync();
           
        }
    }
}

