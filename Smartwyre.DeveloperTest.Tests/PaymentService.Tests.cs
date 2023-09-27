using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Builders;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void FixedRateRebateTestSuccess()
    {
        Assert.True(TestRebateService("1", "1", 10.0m));
    }
    
    [Fact]
    public void FixedRateRebateTestFail()
    {
        Assert.False(TestRebateService("1", "3", 10.0m));
    }
    
    [Fact]
    public void FixedCashAmountTestSuccess()
    {
        Assert.True(TestRebateService("2", "2", 10.0m)); 
    }
    
    [Fact]
    public void FixedCashAmountTestFail()
    {
        Assert.False(TestRebateService("2", "1", 10.0m));
    }

    
    [Fact]
    public void AmountPerUomTestSuccess()
    {
        Assert.True(TestRebateService("3", "3", 10.0m));
    }
    
    [Fact]
    public void AmountPerUomTestFail()
    {
        Assert.False(TestRebateService("3", "5", 10.0m));
    }
    
    private bool TestRebateService(string rebateIdentifier, string productIdentifier, decimal volume)
    {
        var serviceProvider = ConfigureServices();
        var rebateService = serviceProvider.GetService<IRebateService>();
        var rebateRequest = new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier,
            Volume = volume
        };

        var result = rebateService.HandleRequest(rebateRequest);

        return result.Success;
    }
    
    private static IServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddScoped<IRebateDataStore, RebateDataStore>()
            .AddScoped<IProductDataStore, ProductDataStore>()
            .AddScoped<IRebateService, RebateService>()
            .AddTransient<FixedCashAmountBuilder>()
            .AddTransient<FixedRateRebateBuilder>()
            .AddTransient<AmountPerUomBuilder>()
            .AddScoped<IDictionary<IncentiveType, IncentiveTypeBuilder>>(provider =>
            {
                var builders = new Dictionary<IncentiveType, IncentiveTypeBuilder>
                {
                    { IncentiveType.FixedCashAmount, provider.GetRequiredService<FixedCashAmountBuilder>() },
                    { IncentiveType.FixedRateRebate, provider.GetRequiredService<FixedRateRebateBuilder>() },
                    { IncentiveType.AmountPerUom, provider.GetRequiredService<AmountPerUomBuilder>() },
                };
                return builders;
            })
            .BuildServiceProvider();
    }
}
