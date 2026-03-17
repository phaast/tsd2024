using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Investor!");

        // Step 1: Get gold prices
        GoldDataService dataService = new GoldDataService();
        DateTime startDate = new DateTime(2019, 01, 01);
        DateTime endDate = DateTime.Now;

        List<GoldPrice> goldPrices = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();

        if (goldPrices == null || goldPrices.Count == 0)
        {
            Console.WriteLine("No data found. Exiting.");
            return;
        }

        Console.WriteLine($"Retrieved {goldPrices.Count} records. Ready for analysis.");

        // --------Analysis--------

        GoldAnalysisService analysisService = new GoldAnalysisService(goldPrices);
        
        // Example
        var avgPrice = analysisService.GetAveragePrice();
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice, 2), "Average Gold Price Last Half Year");

        // 2.a
        var top3highestMethod = analysisService.GetTop3HighestPricesLastYear_Method();
        GoldResultPrinter.PrintPrices(top3highestMethod, "TOP 3 highest prices last year (Method)");

        var top3lowestMethod = analysisService.GetTop3LowestPricesLastYear_Method();
        GoldResultPrinter.PrintPrices(top3lowestMethod, "TOP 3 lowest prices last year (Method)");

        var top3highestQuery = analysisService.GetTop3HighestPricesLastYear_Query();
        GoldResultPrinter.PrintPrices(top3highestQuery, "TOP 3 highest prices last year (Query)");

        var top3lowestQuery = analysisService.GetTop3LowestPricesLastYear_Query();
        GoldResultPrinter.PrintPrices(top3lowestQuery, "TOP 3 lowest prices last year (Query)");

        
        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");
    }
}
