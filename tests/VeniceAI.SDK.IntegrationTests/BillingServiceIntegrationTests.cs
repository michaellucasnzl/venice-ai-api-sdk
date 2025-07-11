using FluentAssertions;
using VeniceAI.SDK.Models.Billing;

namespace VeniceAI.SDK.IntegrationTests;

/// <summary>
/// Integration tests for the Billing service.
/// </summary>
public class BillingServiceIntegrationTests(ITestOutputHelper output) : IntegrationTestBase(output)
{
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
        var response = await Client.Billing.GetBillingUsageAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Pagination.Should().NotBeNull();
        response.Pagination.Limit.Should().Be(10);
        response.Pagination.Page.Should().Be(1);
        
        Output.WriteLine($"Total usage entries: {response.Pagination.Total}");
        Output.WriteLine($"Total pages: {response.Pagination.TotalPages}");
        Output.WriteLine($"Current page entries: {response.Data.Count}");
        
        if (response.Data.Any())
        {
            Output.WriteLine("\nRecent usage entries:");
            foreach (var entry in response.Data.Take(5))
            {
                Output.WriteLine($"- {entry.Timestamp:yyyy-MM-dd HH:mm:ss}: {entry.Sku} - {entry.Amount} {entry.Currency}");
            }
        }

        await VerifyResult(response);
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
        var response = await Client.Billing.GetBillingUsageAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        
        Output.WriteLine($"Usage entries in last 30 days: {response.Data.Count}");
        Output.WriteLine($"Date range: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
        
        // Verify all entries are within the date range
        foreach (var entry in response.Data)
        {
            entry.Timestamp.Should().BeOnOrAfter(startDate);
            entry.Timestamp.Should().BeOnOrBefore(endDate);
        }
        
        if (response.Data.Any())
        {
            var totalCost = response.Data.Sum(e => e.Amount);
            Output.WriteLine($"Total cost in period: {totalCost:F4}");
            
            var groupedByCurrency = response.Data.GroupBy(e => e.Currency).ToList();
            foreach (var group in groupedByCurrency)
            {
                var currencyTotal = group.Sum(e => e.Amount);
                Output.WriteLine($"Total {group.Key}: {currencyTotal:F4}");
            }
        }

        await VerifyResult(response);
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
        var response = await Client.Billing.GetBillingUsageAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        
        Output.WriteLine($"DIEM usage entries: {response.Data.Count}");
        
        // Verify all entries are in DIEM currency
        foreach (var entry in response.Data)
        {
            entry.Currency.Should().Be(Currency.DIEM);
        }
        
        if (response.Data.Any())
        {
            var totalDiem = response.Data.Sum(e => e.Amount);
            Output.WriteLine($"Total DIEM spent: {totalDiem:F4}");
            
            var skuGroups = response.Data.GroupBy(e => e.Sku).ToList();
            foreach (var group in skuGroups.Take(5))
            {
                var skuTotal = group.Sum(e => e.Amount);
                Output.WriteLine($"{group.Key}: {skuTotal:F4} DIEM");
            }
        }

        await VerifyResult(response);
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
        var page1Response = await Client.Billing.GetBillingUsageAsync(page1Request, TestContext.Current.CancellationToken);
        var page2Response = await Client.Billing.GetBillingUsageAsync(page2Request, TestContext.Current.CancellationToken);

        // Assert
        page1Response.Should().NotBeNull();
        page1Response.IsSuccess.Should().BeTrue();
        page1Response.Pagination.Page.Should().Be(1);
        page1Response.Pagination.Limit.Should().Be(5);
        
        page2Response.Should().NotBeNull();
        page2Response.IsSuccess.Should().BeTrue();
        page2Response.Pagination.Page.Should().Be(2);
        page2Response.Pagination.Limit.Should().Be(5);
        
        Output.WriteLine($"Page 1 entries: {page1Response.Data.Count}");
        Output.WriteLine($"Page 2 entries: {page2Response.Data.Count}");
        
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

        await VerifyResult(new { page1Response, page2Response });
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
        var response = await Client.Billing.GetBillingUsageAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        
        // Look for entries with inference details
        var inferenceEntries = response.Data.Where(e => e.InferenceDetails != null).ToList();
        
        Output.WriteLine($"Entries with inference details: {inferenceEntries.Count}");
        
        foreach (var entry in inferenceEntries.Take(5))
        {
            Output.WriteLine($"\nInference Entry:");
            Output.WriteLine($"  SKU: {entry.Sku}");
            Output.WriteLine($"  Amount: {entry.Amount} {entry.Currency}");
            Output.WriteLine($"  Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
            
            if (entry.InferenceDetails != null)
            {
                Output.WriteLine($"  Request ID: {entry.InferenceDetails.RequestId}");
                Output.WriteLine($"  Prompt Tokens: {entry.InferenceDetails.PromptTokens}");
                Output.WriteLine($"  Completion Tokens: {entry.InferenceDetails.CompletionTokens}");
                Output.WriteLine($"  Execution Time: {entry.InferenceDetails.InferenceExecutionTime}ms");
            }
        }

        await VerifyResult(response);
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
        var ascResponse = await Client.Billing.GetBillingUsageAsync(ascRequest, TestContext.Current.CancellationToken);
        var descResponse = await Client.Billing.GetBillingUsageAsync(descRequest, TestContext.Current.CancellationToken);

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
            
            Output.WriteLine($"Ascending order - First: {ascResponse.Data.First().Timestamp:yyyy-MM-dd HH:mm:ss}");
            Output.WriteLine($"Ascending order - Last: {ascResponse.Data.Last().Timestamp:yyyy-MM-dd HH:mm:ss}");
        }
        
        if (descResponse.Data.Count > 1)
        {
            // Verify descending order
            for (int i = 0; i < descResponse.Data.Count - 1; i++)
            {
                descResponse.Data[i].Timestamp.Should().BeOnOrAfter(descResponse.Data[i + 1].Timestamp);
            }
            
            Output.WriteLine($"Descending order - First: {descResponse.Data.First().Timestamp:yyyy-MM-dd HH:mm:ss}");
            Output.WriteLine($"Descending order - Last: {descResponse.Data.Last().Timestamp:yyyy-MM-dd HH:mm:ss}");
        }

        await VerifyResult(new { ascResponse, descResponse });
    }
}
