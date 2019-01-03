using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.DTO.ApiDTO;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.Repositories;
using RecipeProjectMVC.Services;
using RecipeProjectMVC.Services.Repositories;

namespace RecipeProjectMVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //  This method gets called by the runtime.Use this method to add services to the container.
        //   For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<RecipeDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<IdentityDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

            //    Only needed if login path shoudn't be "/Account/Login" 
            services.ConfigureApplicationCookie(o => o.LoginPath = "/LogIn");

            services.AddTransient<AccountService>();
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<NutritioninfoRepository>();
            services.AddTransient<OrderRepository>();
            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication(); // Identity
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<RecipeDTO, CaloriesDTO>()
                .ForMember(dest => dest.Calories, opt => opt.MapFrom(src => Math.Round(src.Calories.Value)));
                cfg.CreateMap<RecipeDTO, ProteinDTO>()
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Protein")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()));

                cfg.CreateMap<RecipeDTO, CarbsDTO>()
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Carbs")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()));

                cfg.CreateMap<RecipeDTO, FatDTO>()
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Fat")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()));

                cfg.CreateMap<RecipeDTO, SodiumDTO>()
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Sodium")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()));

                cfg.CreateMap<RecipeDTO, CholesterolDTO>()
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Cholesterol")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()));

                cfg.CreateMap<RecipeDTO, VitaminCDTO>()
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Vitamin C")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()));

                cfg.CreateMap<Recipe, RecipeDTO>()
                    .ForMember(dest => dest.Nutritioninfo, opt => opt.MapFrom(src => src.Nutritioninfo.ToList()))
                    .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src.Ingredient.ToList()))
                    .ForMember(dest => dest.HealthLabel, opt => opt.MapFrom(src => src.HealthLabel.ToList()));

            

                cfg.CreateMap<RecipeDTO, LowCarbHighFatDTO>()
                    .ForMember(dest => dest.Fat, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Fat")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()))
                    .ForMember(dest => dest.Carb, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Carbs")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()
                   ));

                cfg.CreateMap<RecipeDTO, HighProteinLowCarbDTO>()
                    .ForMember(dest => dest.Protein, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Protein")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()))
                    .ForMember(dest => dest.Carb, opt => opt.MapFrom(src => src.Nutritioninfo
                    .Where(p => p.Label == "Carbs")
                    .Select(x => Math.Round(x.Total.Value))
                    .FirstOrDefault()
                   ));
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
