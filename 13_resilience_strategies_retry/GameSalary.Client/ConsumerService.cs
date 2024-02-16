using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameSalary.Client;
public class ConsumerService
{
    private readonly HttpClient _httpClient;

    public ConsumerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetTopGameSalariesAsync()
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2),
                (result, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Request failed. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
                });

        var response = await retryPolicy.ExecuteAsync(async () =>
        {
            var httpResponse = await _httpClient.GetAsync("/VideoGameSalaries");
            httpResponse.EnsureSuccessStatusCode();
            return httpResponse;
        });

        return await response.Content.ReadAsStringAsync();
    }
}