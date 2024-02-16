using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using GameSalary.Client;

namespace GameSalaryApi.Tests;

public class GameSalaryServiceTests
{
    [Fact]
    public async Task GetTopGameSalaries_RetriesOnFailureAndSucceeds()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var retryCount = 0;

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(() =>
            {
                retryCount++;
                if (retryCount == 1)
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("[{\"title\": \"Minecraft\", \"salary\": \"1000000\"}]")
                };
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://gamersworld/api/reports/salary"),
        };

        var service = new ConsumerService(httpClient);

        // Act
        var result = await service.GetTopGameSalariesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Minecraft", result);
        Assert.Equal(2, retryCount);
    }
}