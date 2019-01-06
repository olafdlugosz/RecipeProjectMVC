using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecipeProjectMVC.DTO;
using RecipeProjectMVC.Models.Entities;
using RecipeProjectMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using RecipeProjectMVC.Repositories;
using RecipeProjectMVC.Services.Repositories;
using System.Net.Mail;
using System.Net;

namespace RecipeProjectMVC.Services
{
    public class OrderDeploymentService
    {
        private readonly OrderRepository _orderRepo;
        private readonly IRecipeService _recipeService;
        public OrderDeploymentService(OrderRepository orderRepository, IRecipeService recipeService)
        {
            _orderRepo = orderRepository;
            _recipeService = recipeService;
        }

        public void DeployOrder(int id)
        {
            var orderToDeploy = _orderRepo.GetOrderById(id);
            if (orderToDeploy.IsShipped == true) return;

            var orderRecipe = _recipeService.GetRecípe(orderToDeploy.ItemId);
            var orderLabel = orderRecipe.Label;
            var orderBody = orderRecipe.Ingredient;

            var msg = new MailMessage();
            msg.To.Add(orderToDeploy.CustomerEmail);
            msg.From = new MailAddress("recipe.project.lernia@gmail.com");
            msg.Subject = "Your recipe for: " + orderRecipe.Label + " has arrived";
            msg.Body = CreateMessageBody(orderBody);
            msg.IsBodyHtml = true;
            

            try
            {
                string SourceMail = "recipe.project.lernia@gmail.com";
                string Password = "recipeproject666";

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(SourceMail, Password),
                    EnableSsl = true
                };
                
                client.Send(msg);

            }
            catch (Exception e)
            {
                var test = e.ToString();

            }
            _orderRepo.ShipOrder(orderToDeploy.Id);
        }
        private string CreateMessageBody(ICollection<IngredientDTO> ingredients)
        {
            string msgBody = "";
            foreach (var item in ingredients)
            {
                msgBody += item.Text + "<br />";
            }
            return msgBody;
        }
    }
}
