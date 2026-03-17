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

        public List<GoldPrice> GetTop3HighestPricesLastYear()
        {
            var lastYear = DateTime.Now.AddYears(-1);
            return _goldPrices
                .Where(p => p.Date >= lastYear)
                .OrderByDescending(p => p.Price)
                .Take(3)
                .ToList();
        }
    }
}
