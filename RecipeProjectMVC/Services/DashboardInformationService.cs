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
        private List<double> MonthsOfIncidents = new List<double> { 0, 1, 2, 3 };
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
            model.IncidentsToOrdersPearsonsCorrelation = ComputePearsonsCorrelationCoeefficient(Orders, Incidents);
            model.IncidentsToOrdersSpearmansCorrelation = ComputeSpearmansRankCorrelation(Orders.ToArray(), Incidents.ToArray());
            model.IncidentPredictionCount = PredictFutureIncidentCount(MonthsOfIncidents.ToArray(), Incidents.ToArray()); 
            model.IncidentPredictionMonth = PredictFutureIncidentMonth(MonthsOfIncidents.ToArray(), Incidents.ToArray()); 
            if (_cache.Get("Alert") != null)
            {
                var warning = _cache.Get<Alert>("Alert");
                model.Alert = warning.TypeOfIncident;
                model.TimeOfIncident = warning.Sighting;

            }
            else { model.Alert = "No Issues today"; };
            return model;
        }
        //Olafs solution
        public static double ComputePearsonsCorrelationCoeefficient(List<double> x, List<double> y)
        {
            Debug.Assert(x.Count == y.Count);
            var MeanX = x.Average();
            var MeanY = y.Average();

            var AAtoSum = new List<double>();
            var BBtoSum = new List<double>();
            var ABtoSum = new List<double>();

            for (int i = 0; i < x.Count; i++)
            {
                AAtoSum.Add(Math.Pow((x[i] - MeanX), 2));
                BBtoSum.Add(Math.Pow((y[i] - MeanY), 2));
                ABtoSum.Add((x[i] - MeanX) * (y[i] - MeanY));
            }

            var aaSigma = AAtoSum.Sum();
            var bbSigma = BBtoSum.Sum();
            var ab = ABtoSum.Sum();

            var result = ab / Math.Sqrt(aaSigma * bbSigma);

            return result;
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
        public string PredictFutureIncidentCount(double[] xVal, double[] yVal)
        {
            double rsquared;
            double yintercept;
            double slope;
            LinearRegression(xVal, yVal, 0, 3, out rsquared, out yintercept, out slope);

            double nextNumberOfAttacks = yintercept + (slope * 4);
            double nextMonthOfAttack = (nextNumberOfAttacks - yintercept) / slope;

             return $"Next possible number of incidents: {nextNumberOfAttacks}";
           
        }
        public string PredictFutureIncidentMonth(double[] xVal, double[] yVal)
        {
            double rsquared;
            double yintercept;
            double slope;
            LinearRegression(xVal, yVal, 0, 3, out rsquared, out yintercept, out slope);

            double nextNumberOfAttacks = yintercept + (slope * 4);
            double nextMonthOfAttack = (nextNumberOfAttacks - yintercept) / slope;

            return $"Next possible month of incident: January (index from start of data recording: {nextMonthOfAttack}) ";

        }
        public static void LinearRegression(double[] xVals, double[] yVals,
                                        int inclusiveStart, int exclusiveEnd,
                                        out double rsquared, out double yintercept,
                                        out double slope)
        {
            
            Debug.Assert(xVals.Length == yVals.Length); //Assertion Failed
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double ssX = 0;
            double ssY = 0;
            double sumCodeviates = 0;
            double sCo = 0;
            double count = exclusiveEnd - inclusiveStart;

            for (int ctr = inclusiveStart; ctr < exclusiveEnd; ctr++)
            {
                double x = xVals[ctr];
                double y = yVals[ctr];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            double RNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            double RDenom = (count * sumOfXSq - (sumOfX * sumOfX))
             * (count * sumOfYSq - (sumOfY * sumOfY));
            sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            double meanX = sumOfX / count;
            double meanY = sumOfY / count;
            double dblR = RNumerator / Math.Sqrt(RDenom);
            rsquared = dblR * dblR;
            yintercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }
        ////StackOverFlow soluton
        //private static double ComputePearsonsCorrelation(IEnumerable<double> xs, IEnumerable<double> ys)
        //{
        //    // sums of x, y, x squared etc.
        //    double sx = 0.0;
        //    double sy = 0.0;
        //    double sxx = 0.0;
        //    double syy = 0.0;
        //    double sxy = 0.0;

        //    int n = 0;

        //    using (var enX = xs.GetEnumerator())
        //    {
        //        using (var enY = ys.GetEnumerator())
        //        {
        //            while (enX.MoveNext() && enY.MoveNext())
        //            {
        //                double x = enX.Current;
        //                double y = enY.Current;

        //                n += 1;
        //                sx += x;
        //                sy += y;
        //                sxx += x * x;
        //                syy += y * y;
        //                sxy += x * y;
        //            }
        //        }
        //    }
        //    // covariation
        //    double cov = sxy / n - sx * sy / n / n;
        //    // standard error of x
        //    double sigmaX = Math.Sqrt(sxx / n - sx * sx / n / n);
        //    // standard error of y
        //    double sigmaY = Math.Sqrt(syy / n - sy * sy / n / n);

        //    // correlation is just a normalized covariation
        //    return cov / sigmaX / sigmaY;
        //}
    }
}
