using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VeniceAI.SDK;
using VeniceAI.SDK.Extensions;
using VeniceAI.SDK.Models.Billing;
using Xunit;
using Xunit.Abstractions;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Billing service.
/// </summary>
public class BillingServiceIntegrationTests : IDisposable
{
    private readonly IHost _host;
    private readonly IVeniceAIClient _client;
    private readonly ITestOutputHelper _output;

    public BillingServiceIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
        
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<BillingServiceIntegrationTests>();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Debug);
                });
                
                services.AddVeniceAI(context.Configuration);
            });

        _host = hostBuilder.Build();
        _client = _host.Services.GetRequiredService<IVeniceAIClient>();
    }

    [Fact]
    public async Task GetBillingUsageAsync_WithDefaultRequest_ShouldReturnUsage()
    {
        // Arrange
        var request = new BillingUsageRequest
        {
            Limit = 10,
            Page = 1,
            SortOrder = SortOrder.Descending
        };

        // Act
        var response = await _client.Billing.GetBillingUsageAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Pagination.Should().NotBeNull();
        response.Pagination.Limit.Should().Be(10);
        response.Pagination.Page.Should().Be(1);
        
        _output.WriteLine($"Total usage entries: {response.Pagination.Total}");
        _output.WriteLine($"Total pages: {response.Pagination.TotalPages}");
        _output.WriteLine($"Current page entries: {response.Data.Count}");
        
        if (response.Data.Any())
        {
            _output.WriteLine("\nRecent usage entries:");
            foreach (var entry in response.Data.Take(5))
            {
                _output.WriteLine($"- {entry.Timestamp:yyyy-MM-dd HH:mm:ss}: {entry.Sku} - {entry.Amount} {entry.Currency}");
            }
        }
    }

    [Fact]
    public async Task GetBillingUsageAsync_WithDateRange_ShouldReturnFilteredUsage()
    {
        // Arrange
        var endDate = DateTime.UtcNow;
        var startDate = endDate.AddDays(-30); // Last 30 days
        
        var request = new BillingUsageRequest
        {
            StartDate = startDate,
            EndDate = endDate,
            Limit = 50,
            Page = 1,
            SortOrder = SortOrder.Descending
        };

        // Act
        var response = await _client.Billing.GetBillingUsageAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        
        _output.WriteLine($"Usage entries in last 30 days: {response.Data.Count}");
        _output.WriteLine($"Date range: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
        
        // Verify all entries are within the date range
        foreach (var entry in response.Data)
        {
            entry.Timestamp.Should().BeOnOrAfter(startDate);
            entry.Timestamp.Should().BeOnOrBefore(endDate);
        }
        
        if (response.Data.Any())
        {
            var totalCost = response.Data.Sum(e => e.Amount);
            _output.WriteLine($"Total cost in period: {totalCost:F4}");
            
            var groupedByCurrency = response.Data.GroupBy(e => e.Currency).ToList();
            foreach (var group in groupedByCurrency)
            {
                var currencyTotal = group.Sum(e => e.Amount);
                _output.WriteLine($"Total {group.Key}: {currencyTotal:F4}");
            }
        }
    }

    [Fact]
    public async Task GetBillingUsageAsync_WithCurrencyFilter_ShouldReturnFilteredUsage()
    {
        // Arrange
        var request = new BillingUsageRequest
        {
            Currency = Currency.DIEM,
            Limit = 25,
            Page = 1,
            SortOrder = SortOrder.Descending
        };

        // Act
        var response = await _client.Billing.GetBillingUsageAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        
        _output.WriteLine($"DIEM usage entries: {response.Data.Count}");
        
        // Verify all entries are in DIEM currency
        foreach (var entry in response.Data)
        {
            entry.Currency.Should().Be(Currency.DIEM);
        }
        
        if (response.Data.Any())
        {
            var totalDiem = response.Data.Sum(e => e.Amount);
            _output.WriteLine($"Total DIEM spent: {totalDiem:F4}");
            
            var skuGroups = response.Data.GroupBy(e => e.Sku).ToList();
            foreach (var group in skuGroups.Take(5))
            {
                var skuTotal = group.Sum(e => e.Amount);
                _output.WriteLine($"{group.Key}: {skuTotal:F4} DIEM");
            }
        }
    }

    [Fact]
    public async Task GetBillingUsageAsync_WithPagination_ShouldReturnCorrectPages()
    {
        // Test pagination by getting first two pages
        var page1Request = new BillingUsageRequest
        {
            Limit = 5,
            Page = 1,
            SortOrder = SortOrder.Descending
        };

        var page2Request = new BillingUsageRequest
        {
            Limit = 5,
            Page = 2,
            SortOrder = SortOrder.Descending
        };

        // Act
        var page1Response = await _client.Billing.GetBillingUsageAsync(page1Request);
        var page2Response = await _client.Billing.GetBillingUsageAsync(page2Request);

        // Assert
        page1Response.Should().NotBeNull();
        page1Response.IsSuccess.Should().BeTrue();
        page1Response.Pagination.Page.Should().Be(1);
        page1Response.Pagination.Limit.Should().Be(5);
        
        page2Response.Should().NotBeNull();
        page2Response.IsSuccess.Should().BeTrue();
        page2Response.Pagination.Page.Should().Be(2);
        page2Response.Pagination.Limit.Should().Be(5);
        
        _output.WriteLine($"Page 1 entries: {page1Response.Data.Count}");
        _output.WriteLine($"Page 2 entries: {page2Response.Data.Count}");
        
        // Verify pagination info is consistent
        page1Response.Pagination.Total.Should().Be(page2Response.Pagination.Total);
        page1Response.Pagination.TotalPages.Should().Be(page2Response.Pagination.TotalPages);
        
        // Verify different entries (if there are enough entries)
        if (page1Response.Data.Any() && page2Response.Data.Any())
        {
            var page1Ids = page1Response.Data.Select(e => e.Timestamp + e.Sku).ToList();
            var page2Ids = page2Response.Data.Select(e => e.Timestamp + e.Sku).ToList();
            
            page1Ids.Should().NotIntersectWith(page2Ids);
        }
    }

    [Fact]
    public async Task GetBillingUsageAsync_WithInferenceDetails_ShouldReturnDetailedInfo()
    {
        // Arrange
        var request = new BillingUsageRequest
        {
            Limit = 20,
            Page = 1,
            SortOrder = SortOrder.Descending
        };

        // Act
        var response = await _client.Billing.GetBillingUsageAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        
        // Look for entries with inference details
        var inferenceEntries = response.Data.Where(e => e.InferenceDetails != null).ToList();
        
        _output.WriteLine($"Entries with inference details: {inferenceEntries.Count}");
        
        foreach (var entry in inferenceEntries.Take(5))
        {
            _output.WriteLine($"\nInference Entry:");
            _output.WriteLine($"  SKU: {entry.Sku}");
            _output.WriteLine($"  Amount: {entry.Amount} {entry.Currency}");
            _output.WriteLine($"  Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
            
            if (entry.InferenceDetails != null)
            {
                _output.WriteLine($"  Request ID: {entry.InferenceDetails.RequestId}");
                _output.WriteLine($"  Prompt Tokens: {entry.InferenceDetails.PromptTokens}");
                _output.WriteLine($"  Completion Tokens: {entry.InferenceDetails.CompletionTokens}");
                _output.WriteLine($"  Execution Time: {entry.InferenceDetails.InferenceExecutionTime}ms");
            }
        }
    }

    [Fact]
    public async Task GetBillingUsageAsync_WithSortOrder_ShouldReturnSortedResults()
    {
        // Test ascending order
        var ascRequest = new BillingUsageRequest
        {
            Limit = 10,
            Page = 1,
            SortOrder = SortOrder.Ascending
        };

        // Test descending order
        var descRequest = new BillingUsageRequest
        {
            Limit = 10,
            Page = 1,
            SortOrder = SortOrder.Descending
        };

        // Act
        var ascResponse = await _client.Billing.GetBillingUsageAsync(ascRequest);
        var descResponse = await _client.Billing.GetBillingUsageAsync(descRequest);

        // Assert
        ascResponse.Should().NotBeNull();
        ascResponse.IsSuccess.Should().BeTrue();
        
        descResponse.Should().NotBeNull();
        descResponse.IsSuccess.Should().BeTrue();
        
        if (ascResponse.Data.Count > 1)
        {
            // Verify ascending order
            for (int i = 0; i < ascResponse.Data.Count - 1; i++)
            {
                ascResponse.Data[i].Timestamp.Should().BeOnOrBefore(ascResponse.Data[i + 1].Timestamp);
            }
            
            _output.WriteLine($"Ascending order - First: {ascResponse.Data.First().Timestamp:yyyy-MM-dd HH:mm:ss}");
            _output.WriteLine($"Ascending order - Last: {ascResponse.Data.Last().Timestamp:yyyy-MM-dd HH:mm:ss}");
        }
        
        if (descResponse.Data.Count > 1)
        {
            // Verify descending order
            for (int i = 0; i < descResponse.Data.Count - 1; i++)
            {
                descResponse.Data[i].Timestamp.Should().BeOnOrAfter(descResponse.Data[i + 1].Timestamp);
            }
            
            _output.WriteLine($"Descending order - First: {descResponse.Data.First().Timestamp:yyyy-MM-dd HH:mm:ss}");
            _output.WriteLine($"Descending order - Last: {descResponse.Data.Last().Timestamp:yyyy-MM-dd HH:mm:ss}");
        }
    }

    public void Dispose()
    {
        _host?.Dispose();
    }
}
