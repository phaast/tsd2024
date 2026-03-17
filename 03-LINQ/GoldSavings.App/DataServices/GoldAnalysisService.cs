using System;
using System.Collections.Generic;
using System.Linq;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldAnalysisService
    {
        private readonly List<GoldPrice> _goldPrices;

        public GoldAnalysisService(List<GoldPrice> goldPrices)
        {
            _goldPrices = goldPrices;
        }
        public double GetAveragePrice()
        {
            return _goldPrices.Average(p => p.Price);
        }

        public List<GoldPrice> GetTop3HighestPricesLastYear_Method()
        {
            var lastYear = DateTime.Now.AddYears(-1);
            return _goldPrices
                .Where(p => p.Date >= lastYear)
                .OrderByDescending(p => p.Price)
                .Take(3)
                .ToList();
        }

        public List<GoldPrice> GetTop3LowestPricesLastYear_Method()
        {
            var lastYear = DateTime.Now.AddYears(-1);
            return _goldPrices
                .Where(p => p.Date >= lastYear)
                .OrderBy(p => p.Price)
                .Take(3)
                .ToList();
        }

        public List<GoldPrice> GetTop3HighestPricesLastYear_Query()
        {
            var lastYear = DateTime.Now.AddYears(-1);
            var query = from p in _goldPrices
                        where p.Date >= lastYear
                        orderby p.Price descending
                        select p;

            return query.Take(3).ToList();
        }

        public List<GoldPrice> GetTop3LowestPricesLastYear_Query()
        {
            var lastYear = DateTime.Now.AddYears(-1);
            var query = from p in _goldPrices
                        where p.Date >= lastYear
                        orderby p.Price
                        select p;

            return query.Take(3).ToList();
        }

        public List<GoldPrice> GetDaysWithMoreThan5PercentProfitSinceJan2020()
        {
            var firstDayInJan2020 = _goldPrices
                .Where(p => p.Date.Year == 2020 && p.Date.Month == 1)
                .OrderBy(p => p.Date)
                .FirstOrDefault();

            if (firstDayInJan2020 == null)
            {
                return new List<GoldPrice>(); 
            }

            double purchasePrice = firstDayInJan2020.Price;
            double targetPrice = purchasePrice * 1.05; 

            return _goldPrices
                .Where(p => p.Date > firstDayInJan2020.Date && p.Price > targetPrice)
                .ToList();
        }
    }
}
