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
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace RecipeProjectMVC.Services
{
    public class DashBoardInformationService
    {
        private readonly IRecipeService _recipeService;
        private readonly OrderRepository _orderRepo;
        private readonly IMemoryCache _cache;
        private List<double> Orders = new List<double> { 40, 35, 20, 10 };
        private List<double> Incidents = new List<double> { 30, 35, 40, 50 };
        public DashBoardInformationService(IRecipeService recipeService, OrderRepository orderRepository, IMemoryCache cache)
        {
            _recipeService = recipeService;
            _orderRepo = orderRepository;
            _cache = cache;
        }

        public async Task<DashboardInfoVM> GetDashBoardInfoVM()
        {
            var model = new DashboardInfoVM();
            model.Orders = await _orderRepo.GetUnShippedOrders();
            model.ShippedOrders = await _orderRepo.GetShippedOrders();
            model.Top5OrderedRecipes = _recipeService.GetTop5SoldRecipes();
            model.Top5Customers = _orderRepo.GetTop5Customers();
            model.IncidentsToOrdersPearsonsCorrelation = ComputePearsonsCorrelation(Orders, Incidents);
            model.IncidentsToOrdersSpearmansCorrelation = ComputeSpearmansRankCorrelation(Orders.ToArray(), Incidents.ToArray());
            if (_cache.Get("Alert") != null)
            {
                var warning = _cache.Get<Alert>("Alert");
                model.Alert = warning.TypeOfIncident;
                model.TimeOfIncident = warning.Sighting;

            }
            else { model.Alert = "No Issues today"; };
            return model;
        }
        
        private static double ComputePearsonsCorrelation(IEnumerable<double> xs, IEnumerable<double> ys)
        {
            // sums of x, y, x squared etc.
            double sx = 0.0;
            double sy = 0.0;
            double sxx = 0.0;
            double syy = 0.0;
            double sxy = 0.0;

            int n = 0;

            using (var enX = xs.GetEnumerator())
            {
                using (var enY = ys.GetEnumerator())
                {
                    while (enX.MoveNext() && enY.MoveNext())
                    {
                        double x = enX.Current;
                        double y = enY.Current;

                        n += 1;
                        sx += x;
                        sy += y;
                        sxx += x * x;
                        syy += y * y;
                        sxy += x * y;
                    }
                }
            }
            // covariation
            double cov = sxy / n - sx * sy / n / n;
            // standard error of x
            double sigmaX = Math.Sqrt(sxx / n - sx * sx / n / n);
            // standard error of y
            double sigmaY = Math.Sqrt(syy / n - sy * sy / n / n);

            // correlation is just a normalized covariation
            return cov / sigmaX / sigmaY;
        }
        public static double ComputeSpearmansRankCorrelation(double[] X, double[] Y)
        {
            Debug.Assert(X.Length == Y.Length);
            var n = Math.Min(X.Length, Y.Length);
            var list = new List<DataPoint>(n);
            for (var i = 0; i < n; i++)
            {
                list.Add(new DataPoint() { X = X[i], Y = Y[i] });
            }
            var byXList = list.OrderBy(r => r.X).ToArray();
            var byYList = list.OrderBy(r => r.Y).ToArray();
            for (var i = 0; i < n; i++)
            {
                byXList[i].RankByX = i + 1;
                byYList[i].RankByY = i + 1;
            }
            var sumRankDiff
              = list.Aggregate((long)0, (total, r) =>
              total += lsqr(r.RankByX - r.RankByY));
            var rankCorrelation
              = 1 - (double)(6 * sumRankDiff)
              / (n * ((long)n * n - 1));
            return rankCorrelation;
        }
        private class DataPoint
        {
            public double X, Y;
            public int RankByX, RankByY;
        }
        public static long lsqr(long d)
        {
            return d * d;
        }
    }
}
