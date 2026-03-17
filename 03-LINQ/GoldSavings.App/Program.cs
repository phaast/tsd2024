using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;

using System.Diagnostics;
using System.Text.Json;

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

        // --------2. Analysis--------

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

        // 2.b
        var profitableDays = analysisService.GetDaysWithMoreThan5PercentProfitSinceJan2020();
        bool isPossibleToProfit5Pc = profitableDays.Any();
        Console.WriteLine($"\nIs it possible that they would have earned more than 5%? {(isPossibleToProfit5Pc ? "Yes" : "No")}");

        if (isPossibleToProfit5Pc)
        {
            Console.WriteLine($"Days with > 5% profit: {profitableDays.Count}");

            var first10ProfitableDays = profitableDays.Take(10).ToList();
            GoldResultPrinter.PrintPrices(first10ProfitableDays, "First 10 days with > 5% profit");
        }

        // 2.c 
        var top3OfSecondTenPrices2019To2022 = analysisService.GetTop3OfSecondTenPrices2019To2022();
        GoldResultPrinter.PrintPrices(top3OfSecondTenPrices2019To2022, "TOP 3 Second Ten Prices (2019-2022)");

        // 2.d
        var yearlyAverages = analysisService.GetYearlyAveragesFor2020_2023_2024();
        GoldResultPrinter.PrintYearlyAverages(yearlyAverages, "Average gold prices for years: 2020, 2023, 2024");

        // 2.e
        var bestInvestment = analysisService.GetBestInvestmentPeriod2020To2024();
        GoldResultPrinter.PrintInvestmentResult(bestInvestment, "Best investment in 2020-2024");

        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

        // --------3. Saving to XML--------

        GoldXmlExport xmlExporter = new GoldXmlExport();
        string xmlFileName = "gold_prices.xml";
        xmlExporter.ExportToXml(goldPrices, xmlFileName);

        // --------4. Reading from XML--------

        GoldXmlImport xmlImporter = new GoldXmlImport();
        List<GoldPrice> importedPrices = xmlImporter.ImportFromXml(xmlFileName);

        Console.WriteLine($"\nSuccessfully imported {importedPrices.Count} records from XML.");

        Console.WriteLine("\nRunning external query in Python...");

        string tempJsonFile = "temp_gold_data.json";
        string jsonString = JsonSerializer.Serialize(goldPrices);
        File.WriteAllText(tempJsonFile, jsonString);

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python3",
                Arguments = $"query.py {tempJsonFile}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string resultFromPython = reader.ReadToEnd();
                    Console.WriteLine(resultFromPython);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to run Python script: {ex.Message}");
        }
        finally
        {
            if (File.Exists(tempJsonFile))
            {
                File.Delete(tempJsonFile);
            }
        }
    }
}
