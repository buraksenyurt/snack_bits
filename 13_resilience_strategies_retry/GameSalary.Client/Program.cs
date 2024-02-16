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
            BaseAddress = new Uri("http://localhost:5299/VideoGameSalaries")
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
