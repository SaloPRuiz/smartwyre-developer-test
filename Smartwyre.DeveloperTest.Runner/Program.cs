using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Builders;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main()
    {
        var serviceProvider = ConfigureServices();
        var rebateService = serviceProvider.GetService<IRebateService>();

        var rebateId = GetInput("Please enter the code of the rebate: ");
        var productId = GetInput("Please enter the code of the product: ");
        var volume = GetDecimalInput("Please enter the volume: ");

        var rebateRequest = new CalculateRebateRequest
        {
            RebateIdentifier = rebateId,
            ProductIdentifier = productId,
            Volume = volume,
        };

        var result = rebateService.HandleRequest(rebateRequest);
        
        Console.WriteLine(result.Success ? "Rebate calculation stored" : "Can't store the rebate calculation");
    }

    private static string GetInput(string message)
    {
        Console.Write(message);
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be empty or whitespace.");
        }

        return input;
    }

    private static decimal GetDecimalInput(string input)
    {
        Console.Write(input);
        if (decimal.TryParse(Console.ReadLine(), out decimal value))
        {
            return value;
        }

        throw new ArgumentException("Invalid input for volume. Please enter a valid decimal value.");
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