using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.Services;

namespace RecipeProjectMVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RecipeDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IRecipeService, RecipeService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            // app.UseAuthentication(); // use for Identity later..
            AutoMapper.Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<HealthLabel, HealthLabelDTO>();
                //cfg.CreateMap<Ingredient, IngredientDTO>();
                //cfg.CreateMap<Nutritioninfo, NutritioninfoDTO>();
                cfg.CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.Nutritioninfo, opt => opt.MapFrom(src => src.Nutritioninfo.ToList()))
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src.Ingredient.ToList()))
                .ForMember(dest => dest.HealthLabel, opt => opt.MapFrom(src => src.HealthLabel.ToList()));
            }
            );
            AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
