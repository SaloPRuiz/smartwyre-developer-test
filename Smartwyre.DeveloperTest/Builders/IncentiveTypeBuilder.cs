using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Builders;

public abstract class IncentiveTypeBuilder
{
    private readonly IRebateDataStore _rebateDataStore;

    protected IncentiveTypeBuilder(IRebateDataStore rebateDataStore)
    {
        _rebateDataStore = rebateDataStore;
    }

    public abstract CalculateRebateResult Calculate(CalculateRebateResult rebateResult);
    
    public virtual void StoreCalculation(CalculateRebateResult rebateResult)
    {
        if (rebateResult.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebateResult.Rebate, rebateResult.RebateAmount);
        }
    }
}