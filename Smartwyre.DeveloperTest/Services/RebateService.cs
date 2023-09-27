using System;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.Builders;
using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IDictionary<IncentiveType, IncentiveTypeBuilder> _incentiveTypeBuilders;

    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IDictionary<IncentiveType, IncentiveTypeBuilder> incentiveTypeBuilders)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _incentiveTypeBuilders = incentiveTypeBuilders;
    }

    public CalculateRebateResult HandleRequest(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);
        
        if (!_incentiveTypeBuilders.TryGetValue(rebate.Incentive, out var incentiveTypeBuilder))
        {
            throw new InvalidOperationException("Invalid incentive type.");
        }
        
        var result = new CalculateRebateResult
        {
            Rebate = rebate,
            Product = product,
            Volume = request.Volume
        };

        incentiveTypeBuilder.Calculate(result);
        incentiveTypeBuilder.StoreCalculation(result);

        return result;
    }
}