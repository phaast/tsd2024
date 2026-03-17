using System;
using System.Collections.Generic;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public static class GoldResultPrinter
    {
        public static void PrintPrices(List<GoldPrice> prices, string title)
        {
            Console.WriteLine($"\n--- {title} ---");
            foreach (var price in prices)
            {
                Console.WriteLine($"{price.Date:yyyy-MM-dd} - {price.Price} PLN");
            }
        }

        public static void PrintSingleValue<T>(T value, string title)
        {
            Console.WriteLine($"\n{title}: {value}");
        }

        public static void PrintYearlyAverages(List<(int Year, double AveragePrice)> averages, string title)
        {
            Console.WriteLine($"\n--- {title} ---");
            foreach (var avg in averages)
            {
                Console.WriteLine($"{avg.Year} - {Math.Round(avg.AveragePrice, 2)} PLN");
            }
        }

        public static void PrintInvestmentResult((GoldPrice BuyDay, GoldPrice SellDay, double ROI) result, string title)
        {
            Console.WriteLine($"\n--- {title} ---");
            if (result.BuyDay == null || result.SellDay == null)
            {
                Console.WriteLine("No sufficient data to calculate investments.");
                return;
            }

            Console.WriteLine($"Buy Day: {result.BuyDay.Date:yyyy-MM-dd} (Price: {result.BuyDay.Price} PLN)");
            Console.WriteLine($"Sell Day: {result.SellDay.Date:yyyy-MM-dd} (Price: {result.SellDay.Price} PLN)");
            Console.WriteLine($"Return from a single purchase: {Math.Round(result.SellDay.Price - result.BuyDay.Price, 2)} PLN");
            Console.WriteLine($"ROI: {Math.Round(result.ROI, 2)} %");
        }
    }
}