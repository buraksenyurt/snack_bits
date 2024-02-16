using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameSalary.Client;
class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://turbo-guide-x9v54pj44xcvp4p-5299.app.github.dev/")
        };

        var consumerService = new ConsumerService(httpClient);

        try
        {
            string result = await consumerService.GetTopGameSalariesAsync();

            Console.WriteLine("Top Game Salaries:");
            Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
