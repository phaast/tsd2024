using Newtonsoft.Json;
using GoldSavings.App.Model;

namespace GoldSavings.App.Client;

public class GoldClient
{
    private HttpClient _client;
    public GoldClient()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://api.nbp.pl/api/");
        _client.DefaultRequestHeaders.Accept.Clear();
    }
    public async Task<GoldPrice> GetCurrentGoldPrice()
    {
        try
        {
            HttpResponseMessage responseMsg = _client.GetAsync("cenyzlota/").GetAwaiter().GetResult();
            if (responseMsg.IsSuccessStatusCode)
            {
                string content = await responseMsg.Content.ReadAsStringAsync();
                List<GoldPrice>? prices = JsonConvert.DeserializeObject<List<GoldPrice>>(content);
                if (prices != null && prices.Count == 1)
                {
                    return prices[0];
                }
            }
            return null;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"API Request Error: {e.Message}");
            return null;
        }


    }

    public async Task<List<GoldPrice>> GetGoldPrices(DateTime startDate, DateTime endDate)
    {
        string dateFormat = "yyyy-MM-dd";
        List<GoldPrice> allPrices = new List<GoldPrice>();

        DateTime currentStartDate = startDate;

        while (currentStartDate <= endDate)
        {
            DateTime currentEndDate = currentStartDate.AddDays(92);

            if (currentEndDate > endDate)
            {
                currentEndDate = endDate;
            }

            string requestUri = $"cenyzlota/{currentStartDate.ToString(dateFormat)}/{currentEndDate.ToString(dateFormat)}";

            HttpResponseMessage responseMsg = _client.GetAsync(requestUri).GetAwaiter().GetResult();

            if (responseMsg.IsSuccessStatusCode)
            {
                string content = await responseMsg.Content.ReadAsStringAsync();
                List<GoldPrice> prices = JsonConvert.DeserializeObject<List<GoldPrice>>(content);

                if (prices != null)
                {
                    allPrices.AddRange(prices);
                }
            }

            else if (responseMsg.StatusCode == System.Net.HttpStatusCode.NotFound) { }

            else
            {
                Console.WriteLine($"[Warning] Could not fetch data from API for period {currentStartDate:yyyy-MM-dd} - {currentEndDate:yyyy-MM-dd}. Code: {responseMsg.StatusCode}");
            }

            currentStartDate = currentEndDate.AddDays(1);
        }

        return allPrices;
    }

}