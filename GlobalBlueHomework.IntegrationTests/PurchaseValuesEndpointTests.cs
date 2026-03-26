using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GlobalBlueHomework.IntegrationTests;

public class PurchaseValuesEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PurchaseValuesEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostPurchaseValues_ShouldReturnOk_WhenRequestIsValid()
    {
        var request = new
        {
            netValue = 100m,
            vatRate = 20
        };

        var response = await _client.PostAsJsonAsync("/purchaseValues", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostPurchaseValues_ShouldReturnBadRequest_WhenMoreThanOneAmountIsProvided()
    {
        var request = new
        {
            netValue = 100m,
            grossValue = 120m,
            vatRate = 20
        };

        var response = await _client.PostAsJsonAsync("/purchaseValues", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostPurchaseValues_ShouldReturnBadRequest_WhenVatRateIsMissing()
    {
        var request = new
        {
            netValue = 100m
        };

        var response = await _client.PostAsJsonAsync("/purchaseValues", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}